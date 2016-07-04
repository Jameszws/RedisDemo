using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace RedisDemo.Models
{
    public class User
    {
        public string Id { get; set; }

        public string UserNo { get; set; }

        public string UserName { get; set; }

        public Job Job { get; set; }
    }
}