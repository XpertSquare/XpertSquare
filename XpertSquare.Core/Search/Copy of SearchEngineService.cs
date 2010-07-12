using System;
using System.Collections.Generic;
using System.IO;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Store;
using log4net;

using XpertSquare.Core.Repository;
using XpertSquare.Core.Model;
using Lucene.Net.Search;
using Lucene.Net.Analysis;
using Lucene.Net.QueryParsers;

namespace XpertSquare.Core.Search
{
    
    public class SearchEngineService : ISearchEngineService
    {
        private const String LUCENE_INDEX_DIRECTORY = "LuceneIndex";
        private const String SEARCH_TITLE = "TITLE";
        private const String SEARCH_CONTENT = "CONTENT";
        private const String SEARCH_TAGS = "TAGS";
        private const String SEARCH_ID = "ID";

        
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

        IList<XsQuestion> ISearchEngineService.SearchQuestions(string searchQuery)
        {
            IList<XsQuestion> searchQuestions = new List<XsQuestion>();


            if (!String.IsNullOrEmpty(searchQuery))
            {
                IndexSearcher searcher = this.Searcher;                
                MultiFieldQueryParser multiParser = new MultiFieldQueryParser(
                    new string[] { SEARCH_TITLE, SEARCH_CONTENT, SEARCH_TAGS }
                    , _analyzer);
                Query queryObject = multiParser.Parse(searchQuery);

                Hits hits = searcher.Search(queryObject);
                for (int i = 0; i < hits.Length(); i++)
                {
                    Document doc = hits.Doc(i);
                    float score = hits.Score(i);
                    searchQuestions.Add(_questionRepository.GetById(Convert.ToInt32(doc.Get("ID"))));
                }
            }
            return searchQuestions;
        }

        IList<XpertSquare.Core.Model.XsQuestion> ISearchEngineService.SearchQuestions(string searchQuery, short numberOfResults)
        {
            throw new NotImplementedException();
        }

        IList<XpertSquare.Core.Model.XsQuestion> ISearchEngineService.SearchRelatedQuestions(XpertSquare.Core.Model.XsQuestion question)
        {
            throw new NotImplementedException();
        }

        IList<XpertSquare.Core.Model.XsQuestion> ISearchEngineService.SearchRelatedQuestions(XpertSquare.Core.Model.XsQuestion question, short numberOfResults)
        {
            throw new NotImplementedException();
        }

        IList<IndexingError> ISearchEngineService.AddQuestionToIndex(XpertSquare.Core.Model.XsQuestion question)
        {
            throw new NotImplementedException();
        }

        IList<IndexingError> ISearchEngineService.AddQuestionsToIndex(IList<XpertSquare.Core.Model.XsQuestion> questions)
        {

            IList<IndexingError> errorList = new List<IndexingError>();
            foreach (var question in questions)
            {
                ExecuteRemovePost(question.ID);
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

            return errorList;
        }

        public void BuildIndex()
        {
            if (!IndexReader.IndexExists(_directoryPath))
            {

                              
                IList<IndexingError> indexErrors = new List<IndexingError>(); ;
                IndexWriter searchIndexWriter = null;
                lock (writerLock)
                {
                    try
                    {
                        String indexPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LUCENE_INDEX_DIRECTORY);
                        EnsureIndexWriter();
                        AddQuestionsToIndex(_writer);
                    }
                    catch (Exception ex)
                    {
                        _log.Error(ex);
                    }
                    finally
                    {
                        //If there is an IO exception searchIndexWriter may be null;
                        try
                        {
                            searchIndexWriter.Optimize();
                            searchIndexWriter.Close();
                        }
                        catch { }
                    }
                }
            }
        }

        private IList<IndexingError> AddQuestionsToIndex(IndexWriter searchIndexWriter)
        {
            IList<IndexingError> indexErrors = new List<IndexingError>();
            foreach (XsQuestion question in _questionRepository.GetAll())
            {
                if (null != question)
                {
                    try
                    {
                        AddQuestionToIndex(searchIndexWriter, question);
                    }
                    catch (System.Exception ex)
                    {
                        indexErrors.Add(new IndexingError() { Exception = ex, Question = question });
                    }
                }
            }
            return indexErrors;
        }

        private void AddQuestionToIndex(IndexWriter searchIndexWriter, XsQuestion question)
        {
            searchIndexWriter.AddDocument(CreateDocument(question));
        }



        private Document CreateDocument(XsQuestion question)
        {
            Document doc = new Document();
            doc.Add(new Field(SEARCH_ID, question.ID.ToString(), Field.Store.YES, Field.Index.NO));
            Field titleField = new Field(SEARCH_TITLE, question.Title, Field.Store.NO, Field.Index.TOKENIZED);
            titleField.SetBoost(5.0f);
            doc.Add(titleField);

            doc.Add(new Field(SEARCH_CONTENT, question.Content, Field.Store.NO, Field.Index.TOKENIZED));

            Field tagField = new Field(SEARCH_TAGS, question.AllTags, Field.Store.NO, Field.Index.TOKENIZED);
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
                if (IndexWriter.IsLocked(this._directoryPath))
                {
                    _log.Error("Something left a lock in the index folder: deleting it");
                    Lucene.Net.Store.Directory directory = FSDirectory.GetDirectory(_directoryPath);
                    IndexWriter.Unlock(directory);
                    _log.Info("Lock Deleted... can proceed");
                }
                _writer = new IndexWriter(this._directoryPath, this._analyzer, IndexWriter.MaxFieldLength.UNLIMITED);
                _writer.SetMergePolicy(new LogDocMergePolicy(_writer));
                _writer.SetMergeFactor(5);
            }
        }

        private static Query GetIdSearchQuery()
{
                return new TermQuery(new Term("ID"));
        }



        private void ExecuteRemovePost(long entryId)
{
            Query searchQuery = GetIdSearchQuery();
            this.DoWriterAction(delegate (IndexWriter writer) {
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
