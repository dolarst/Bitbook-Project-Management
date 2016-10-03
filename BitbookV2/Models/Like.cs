using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
    public class Like
    {
        public int Id { set; get; }

        public int PostId { set; get; }

        public int RegistrationId { set; get; }
        public Registration Registration { set; get; }

    }
}