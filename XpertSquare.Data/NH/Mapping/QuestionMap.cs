using System;

using XpertSquare.Core.Model;
using FluentNHibernate.Mapping;

namespace XpertSquare.Data.NH.Mapping
{
    public class QuestionMap : ClassMap<XsQuestion>
    {
        private const String HiLoBatchSize = "1";
        public QuestionMap()
        {
            Table("QUESTION");
            Id(x => x.ID, "QUESTION_ID")
                .GeneratedBy.HiLo("NH_HILO", "NEXT_ID", HiLoBatchSize,
                p => p.AddParam("where", "TABLENAME='QUESTION'"));
            Map(x => x.CreationDT, "CREATION_DT");
            Map(x => x.UpdateDT, "UPDATE_DT");
            Map(x => x.LastUpdator, "LAST_UPDATOR")
                .Length(50);
            Map(x =>x.Title, "TITLE")
               .Length(140)
               .Not.Nullable();
            Map(x => x.SlugTitle, "SLUG_TITLE")
               .Length(140)
               .Not.Nullable();
            Map(x => x.Content, "CONTENT")
                .Length(2000)
                .Not.Nullable();
            Map(x => x.ContentHtml, "CONTENT_HTML")
               .Length(2000)
               .Not.Nullable();
            Map(x => x.Excerpt, "EXCERPT")
                .Length(200);
            Map(x => x.Status, "STATUS")
                .CustomType("Int32");
            Map(x => x.Ranking, "RANKING");
            References(x => x.Author)
                .Column("AUTHOR_ID")
                .Cascade.All();
            HasMany(x => x.Answers)
                .KeyColumn("QUESTION_ID")
                .Cascade.All();
            HasMany(x => x.Votes)
               .KeyColumn("QUESTION_ID")
               .Cascade.All();
            HasMany(x => x.History)
                .KeyColumn("QUESTION_ID")
                .Cascade.All();
            HasManyToMany(x => x.Tags)
                .Table("QUESTION_TAG")
                .ParentKeyColumn("QUESTION_ID")
                .ChildKeyColumn("TAG_ID")
                .Cascade.All();
            
        }
    }
}
