using System;
using FluentNHibernate.Mapping;
using XpertSquare.Core.Model;


namespace XpertSquare.Data.NH.Mapping
{
    public class QuestionHistoryMap : ClassMap<XsQuestionHistory>
    {
        public QuestionHistoryMap()
        {
            Table("QUESTION_HISTORY");
            Id(x => x.ID, "HISTORY_ID")
                .GeneratedBy.Assigned();
            Map(x => x.CreationDT, "CREATION_DT");
            Map(x => x.UpdateDT, "UPDATE_DT");
            Map(x => x.LastUpdator, "LAST_UPDATOR")
                .Length(50);
            Map(x => x.EditSummary, "EDIT_SUMMARY")
               .Length(140)
               .Not.Nullable();
            Map(x => x.Title, "TITLE")
               .Length(140)
               .Not.Nullable();
            Map(x => x.Content, "CONTENT")
                .Length(2000)
                .Not.Nullable();
            Map(x => x.ContentHtml, "CONTENT_HTML")
               .Length(2000)
               .Not.Nullable();
            Map(x => x.AllTags, "TAGS")
                .Length(250)
                .Not.Nullable();
        }
    }
}
