using System;

using XpertSquare.Core.Model;
using FluentNHibernate.Mapping;

namespace XpertSquare.Data.NH.Mapping
{
    public class TagMap:ClassMap<XsTag>
    {
        private const String HiLoBatchSize = "5";
        public TagMap()
        {
            Table("TAG");
            Id(x => x.ID, "TAG_ID")
                 .GeneratedBy.HiLo("NH_HILO", "NEXT_ID", HiLoBatchSize,
                p => p.AddParam("where", "TABLENAME='TAG'"));
            Map(x => x.Name, "NAME")
                .Length(50);
            Map(x => x.QuestionCount, "QUESTIONS_COUNT");
            HasManyToMany(x => x.Questions)              
                .AsBag()
                .Table("QUESTION_TAG")
                .ParentKeyColumn("TAG_ID")
                .ChildKeyColumn("QUESTION_ID")                
                .Inverse();
        }
    }
}
