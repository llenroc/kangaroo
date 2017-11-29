using Hidistro.Core.Entities;

namespace Hidistro.Entites.Kangaroo
{
    public class CardInfoQuery : Pagination
    {

        public string CardNumber
        {
            get;
            set;
        }
        public string UserName
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }


        public string UserPhone
        {
            get;
            set;
        }

        public string StartTime
        {
            get;
            set;
        }

        public string EndTime
        {
            get;
            set;
        }
    }
}
