using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Store;
using Lucene.Net.Search;
using Lucene.Net.Analysis;
using Lucene.Net.QueryParsers;


using log4net;

using XpertSquare.Core.Repository;
using XpertSquare.Core.Model;

namespace XpertSquare.Core.Search
{
    public class SearchEngineService : ISearchEngineService
    {
        private const String LUCENE_INDEX_DIRECTORY = "LuceneIndex";
        private const String SEARCH_TITLE = "TITLE";
        private const String SEARCH_CONTENT = "CONTENT";
        private const String SEARCH_TAGS = "TAGS";
        private const String SEARCH_ID = "ID";
        private const Int16 MAX_RELATED_QUESTIONS = 20;

        private bool _disposed = false;
        private String _directoryPath;
        private static readonly Object writerLock = null;

        private static IndexWriter _writer;
        private Analyzer _analyzer = null;


        private static readonly ILog _log = LogManager.GetLogger(typeof(SearchEngineService));

        private IQuestionRepository _questionRepository = null;

        #region ISearchEngineService Members

        static SearchEngineService()
        {
            writerLock = new Object();
        }

        public SearchEngineService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
            _analyzer = new StandardAnalyzer();
            _directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LUCENE_INDEX_DIRECTORY);
        }

        public IList<XsQuestion> SearchQuestions(string searchQuery)
        {

            return SearchQuestions(searchQuery, Int16.MaxValue);

        }

        public IList<XpertSquare.Core.Model.XsQuestion> SearchQuestions(String searchQuery, Int16 numberOfResults)
        {
            _log.Debug("SEARCH: SearchQs starts at: " + DateTime.Now.ToLongTimeString() + " : " + DateTime.Now.Millisecond.ToString());
            IList<XsQuestion> searchQuestions = new List<XsQuestion>();


            if (!String.IsNullOrEmpty(searchQuery))
            {
                IndexSearcher searcher = this.Searcher;
                MultiFieldQueryParser multiParser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29,
                    new string[] { SEARCH_TITLE, SEARCH_CONTENT, SEARCH_TAGS }
                    , _analyzer);
                Query queryObject = multiParser.Parse(searchQuery);

                TopDocs searchResults = searcher.Search(queryObject, numberOfResults);
                for (int i = 0; i < searchResults.scoreDocs.Length; i++)
                {
                    Document doc = searcher.Doc(searchResults.scoreDocs[i].doc);
                    float score = searchResults.scoreDocs[i].score;
                    XsQuestion question = _questionRepository.GetById(Convert.ToInt32(doc.Get(SEARCH_ID)));
                    if (null != question)
                    {
                        searchQuestions.Add(question);
                    }
                }
                searcher.Close();
            }
            _log.Debug("SEARCH: SearchQs ends at: " + DateTime.Now.ToLongTimeString() + " : " + DateTime.Now.Millisecond.ToString());

            return searchQuestions;
        }

        public IList<XpertSquare.Core.Model.XsQuestion> SearchRelatedQuestions(XpertSquare.Core.Model.XsQuestion question)
        {
            return SearchRelatedQuestions(question, MAX_RELATED_QUESTIONS);
        }

        public IList<XpertSquare.Core.Model.XsQuestion> SearchRelatedQuestions(XpertSquare.Core.Model.XsQuestion question, short numberOfResults)
        {
            IList<XsQuestion> relatedQuestions = new List<XsQuestion>();

            IList<XsQuestion> allQuestions = SearchQuestions(question.AllTags, numberOfResults);
            foreach (XsQuestion item in allQuestions)
            {
                if ((null != item) && question.ID != item.ID)
                {
                    relatedQuestions.Add(item);
                }
            }

            return relatedQuestions;
        }

        public IList<IndexingError> AddQuestionToIndex(XpertSquare.Core.Model.XsQuestion question)
        {
            return AddQuestionsToIndex(new List<XsQuestion>() { question });
        }

        public IList<IndexingError> AddQuestionsToIndex(IList<XpertSquare.Core.Model.XsQuestion> questions)
        {
            _log.Debug("SEARCH: AddQs starts at: " + DateTime.Now.ToLongTimeString() + " : " + DateTime.Now.Millisecond.ToString());

            IList<IndexingError> errorList = new List<IndexingError>();
            foreach (var question in questions)
            {
                ExecuteRemoveQuestion(question.ID);
                try
                {
                    var currentQuestion = question;
                    DoWriterAction(writer => writer.AddDocument(CreateDocument(currentQuestion)));
                }
                catch (Exception ex)
                {
                    errorList.Add(new IndexingError() { Question = question, Exception = ex });
                }
            }
            DoWriterAction(writer =>
            {
                writer.Commit();
                writer.Optimize();
            });
            _log.Debug("SEARCH: AddQs ends at: " + DateTime.Now.ToLongTimeString() + " : " + DateTime.Now.Millisecond.ToString());
            return errorList;
        }

        public void BuildIndex()
        {
            _log.Debug("SEARCH: BuildIndex starts at: " + DateTime.Now.ToLongTimeString() + " : " + DateTime.Now.Millisecond.ToString());
            //Lucene.Net.Store.Directory directory = FSDirectory.GetDirectory(_directoryPath);
            if (!System.IO.Directory.Exists(_directoryPath))
            {
                System.IO.Directory.CreateDirectory(this._directoryPath);
                IList<IndexingError> indexErrors = new List<IndexingError>(); ;

                IList<XsQuestion> allQuestions
                   = (from question in _questionRepository.GetAll()
                      select question).ToList();

                AddQuestionsToIndex(allQuestions);
            }
            _log.Debug("SEARCH: BuildIndex ends at: " + DateTime.Now.ToLongTimeString() + " : " + DateTime.Now.Millisecond.ToString());
        }
               
        private void AddQuestionToIndex(IndexWriter searchIndexWriter, XsQuestion question)
        {
            searchIndexWriter.AddDocument(CreateDocument(question));
        }

        private Document CreateDocument(XsQuestion question)
        {
            Document doc = new Document();
            doc.Add(new Field(
                SEARCH_ID
                , question.ID.ToString()
                , Field.Store.YES
                , Field.Index.NOT_ANALYZED
                ,Field.TermVector.NO));
            Field titleField = new Field(
                SEARCH_TITLE
                , question.Title
                , Field.Store.NO
                , Field.Index.ANALYZED
                , Field.TermVector.YES);
            titleField.SetBoost(5.0f);
            doc.Add(titleField);

            doc.Add(new Field(
                SEARCH_CONTENT
                , question.Content
                , Field.Store.NO
                , Field.Index.ANALYZED
                , Field.TermVector.YES));

            Field tagField = new Field(
                SEARCH_TAGS
                , question.AllTags
                , Field.Store.NO
                , Field.Index.ANALYZED
                , Field.TermVector.YES);
            tagField.SetBoost(10.0f);
            doc.Add(tagField);

            return doc;
        }

        private IndexSearcher Searcher
        {
            get { return DoWriterAction(writer => new IndexSearcher(writer.GetReader())); }
        }

        private void DoWriterAction(Action<IndexWriter> action)
        {
            lock (writerLock)
            {
                EnsureIndexWriter();
            }
            action(_writer);
        }

        private T DoWriterAction<T>(Func<IndexWriter, T> action)
        {
            lock (writerLock)
            {
                EnsureIndexWriter();
            }
            return action(_writer);
        }


        private void EnsureIndexWriter()
        {
            if (_writer == null)
            {
                Lucene.Net.Store.Directory directory = FSDirectory.Open(new DirectoryInfo(this._directoryPath));
                if (IndexWriter.IsLocked(directory))
                {
                    _log.Error("Something left a lock in the index folder: deleting it");
                    IndexWriter.Unlock(directory);
                    _log.Info("Lock Deleted... can proceed");
                }
                _writer = new IndexWriter(this._directoryPath, this._analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
                _writer.SetMergePolicy(new LogDocMergePolicy(_writer));
                _writer.SetMergeFactor(5);
            }
        }

        private static Query GetIdSearchQuery(long questionID)
        {
            return new TermQuery(new Term(SEARCH_ID,questionID.ToString()));
        }

        private void ExecuteRemoveQuestion(long questionID)
        {
            Query searchQuery = GetIdSearchQuery(questionID);
            this.DoWriterAction(delegate(IndexWriter writer)
            {
                writer.DeleteDocuments(searchQuery);
            });
        }



        ~SearchEngineService()
        {
            Dispose();
        }


        public void Dispose()
        {
            lock (writerLock)
            {
                if (!this._disposed)
                {
                    IndexWriter writer = _writer;
                    if (writer != null)
                    {
                        try
                        {
                            writer.Close();
                        }
                        catch (ObjectDisposedException exception)
                        {
                            _log.Error("Exception while disposing SearchEngineService", exception);
                        }
                        _writer = null;
                    }
                    this._disposed = true;
                }
            }
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
