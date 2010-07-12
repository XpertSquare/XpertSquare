using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using XpertSquare.Web.Models;
using XpertSquare.Web.Core;
using XpertSquare.Core.Model;
using XpertSquare.Core.Repository;
using XpertSquare.Data.NH;
using XpertSquare.Data.NH.Repository;

using log4net;

namespace XpertSquare.Web.Controllers
{
    public class FilterController : XsController
    {
        private String SPACE = " ";
        private Char SPACE_CHAR = ' ';

        private static readonly ILog log = LogManager.GetLogger(typeof(FilterController));

        private ITagRepository tagRepository = null;

        public FilterController()
        {
            tagRepository = new TagRepository();
        }

        public FilterController(IRepositoryFactory repositoryFactory)
        {
            tagRepository = repositoryFactory.GetTagRepository();
        }


        //
        // GET: /Filter/AutoComplete

        public JsonResult AutoComplete(String query)
        {

            AutocompleteResultModel result = null;

            IList<String> tagSuggestions = new List<String>();
            IList<String> tagData = new List<String>();

            if (query.EndsWith(SPACE))
            {
                result = new AutocompleteResultModel()
                {
                    query = query,
                    suggestions = tagSuggestions,
                    data = tagData
                };
            }
            else
            {
                String[] queryTags = query.Split(SPACE_CHAR);
                String lastTag = queryTags[queryTags.Length - 1].ToLowerInvariant();

                Int32 lastSpacePosition = query.LastIndexOf(SPACE_CHAR);
                String rootTags = String.Empty;
                if (lastSpacePosition > 0)
                {
                    rootTags = query.Substring(0, lastSpacePosition+1);
                }

                IList<XsTag> allTags = (from tag in tagRepository.GetAll()
                                        where tag.Name.StartsWith(lastTag)                                        
                                        select tag).ToList();


                Int32 tagIndex = 0;
                while ((tagIndex < Settings.AUTOCOMPLETE_ENTRIES) && (tagIndex < allTags.Count))
                {
                    XsTag currentTag = allTags[tagIndex];
                    tagSuggestions.Add(String.Concat(rootTags, currentTag.Name));
                    tagData.Add(currentTag.ID.ToString());
                    tagIndex++;
                }

                result = new AutocompleteResultModel()
                {
                    query = query,
                    suggestions = tagSuggestions,
                    data = tagData
                };
            }


            JsonResult jsonResult = this.Json(result);

            return jsonResult;
        }


    }
}
