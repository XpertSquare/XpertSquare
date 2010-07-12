using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Model
{
    public class XsUser : XsBusinessObject
    {
        ///Fields
        ///UserID - string
        ///password - string
        ///Firstname
        ///Lastname
        ///Description
        ///Location
        ///


        private long _id;
        private String _username = String.Empty;
        private String _password = String.Empty;
        private String _email = String.Empty;
        private String _displayName = String.Empty;
        private String _firstname = String.Empty;
        private String _lastname = String.Empty;
        private String _description = String.Empty;
        private String _location = String.Empty;
        private DateTime _lastActivityDT;
        private Int32 _ranking = 0;
        private Int16 _questionCount = 0;
        private Int16 _answerCount = 0;
        private TimeZone _timezone = null;


        public virtual long ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual String Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public virtual String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public virtual String Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public virtual String DisplayName
        {
            get { return _displayName;  }
            set { _displayName = value; }
        }

        public virtual String FirstName
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        public virtual String LastName
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        public virtual String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual String Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public virtual DateTime LastActivityDT
        {
            get { return _lastActivityDT; }
            set { _lastActivityDT = value; }
        }

        public virtual Int32 Ranking
        {
            get { return _ranking; }
            set { _ranking = value; }

        }

        public virtual Int16 QuestionCount
        {
            get { return _questionCount; }
            set { _questionCount = value; }

        }

        public virtual Int16 AnswerCount
        {
            get { return _answerCount; }
            set { _answerCount = value; }

        }

        public XsUser()
        {
            CreationDT = DateTime.UtcNow;
            UpdateDT = DateTime.UtcNow;
            LastActivityDT = DateTime.UtcNow;
        }

       
    }
}
