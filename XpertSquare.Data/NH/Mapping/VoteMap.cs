using System;

using XpertSquare.Core.Model;
using FluentNHibernate.Mapping;

namespace XpertSquare.Data.NH.Mapping
{
    public class VoteMap : ClassMap<XsVote>
    {
        private const String HiLoBatchSize = "1";
        public VoteMap()
        {
            Table("VOTE");
            Id(x=>x.ID, "VOTE_ID")
                 .GeneratedBy.HiLo("NH_HILO", "NEXT_ID", HiLoBatchSize,
                p => p.AddParam("where", "TABLENAME='VOTE'"));
            Map(x => x.CreationDT, "CREATION_DT");
            Map(x => x.UpdateDT, "UPDATE_DT");
            Map(x => x.LastUpdator, "LAST_UPDATOR")
                .Length(50);
            Map(x => x.Value, "VALUE");
            References(x => x.Answer)
                .Column("ANSWER_ID");
            References(x => x.Question)
                .Column("QUESTION_ID");
            References(x => x.User)
                .Column("USER_ID");

        }
    }
}
