using System;

using XpertSquare.Core.Model;
using FluentNHibernate.Mapping;

namespace XpertSquare.Data.NH.Mapping
{
    public class AnswerMap : ClassMap<XsAnswer>
    {
        private const String HiLoBatchSize = "1";
        public AnswerMap()
        {
            Table("ANSWER");
            Id(x => x.ID, "ANSWER_ID")
                .GeneratedBy.HiLo("NH_HILO", "NEXT_ID", HiLoBatchSize,
                p => p.AddParam("where", "TABLENAME='ANSWER'"));
            Map(x => x.CreationDT, "CREATION_DT");
            Map(x => x.UpdateDT, "UPDATE_DT");
            Map(x => x.LastUpdator, "LAST_UPDATOR")
                .Length(50);
            Map(x => x.Content, "CONTENT")
                .Length(2000)
                .Not.Nullable();
            Map(x => x.ContentHtml, "CONTENT_HTML")
                .Length(2000)
                .Not.Nullable();
            References(x => x.Author)
                .Column("AUTHOR_ID")
                .Cascade.All();
            References(x => x.Parent)
                .Column("QUESTION_ID")
                .Cascade.All();
            HasMany(x => x.Votes)
                .KeyColumn("ANSWER_ID")
                .Cascade.All();
            // TODO  - complete mapping
        }
    }
}
