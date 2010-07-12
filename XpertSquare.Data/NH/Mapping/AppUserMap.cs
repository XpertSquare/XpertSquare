using System;

using XpertSquare.Core.Model;
using FluentNHibernate.Mapping;

namespace XpertSquare.Data.NH.Mapping
{
    public class XsUserMap : ClassMap<XsUser>
    {
        private const String HiLoBatchSize = "1";
        public XsUserMap()
        {
            Table("APP_USER");
            Id(x=>x.ID,"USER_ID")
                .GeneratedBy.HiLo("NH_HILO", "NEXT_ID", HiLoBatchSize,
                p => p.AddParam("where", "TABLENAME='APP_USER'"));
            Map(x => x.CreationDT, "CREATION_DT");
            Map(x => x.UpdateDT, "UPDATE_DT");            
            Map(x => x.LastUpdator, "LAST_UPDATOR")
                .Length(50);
            Map(x => x.FirstName, "FIRST_NAME")
                .Length(50);
            Map(x => x.LastName, "LAST_NAME")
                .Length(50);
            Map(x => x.DisplayName, "DISPLAY_NAME")
                .Length(50);
            Map(x => x.Username, "USERNAME")
                .Length(50);
            Map(x => x.Password, "PWD")
                .Length(50);
            Map(x => x.Email, "EMAIL")
                .Length(100);
            Map(x => x.Description, "DESCRIPTION")
                .Length(2000);
            Map(x => x.Location, "LOCATION")
                .Length(200);
            Map(x => x.QuestionCount, "QUESTION_COUNT");
            Map(x => x.AnswerCount, "ANSWER_COUNT");
            Map(x => x.LastActivityDT, "LAST_ACTIVITY_DT");
        }
    }
}
