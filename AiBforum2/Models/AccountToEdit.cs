using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiBforum2.Models
{
    public class AccountToEdit
    {
        public Account account { get; set; }
        public System.Web.Mvc.SelectList groups;

        public AccountToEdit()
        {
        }

        public AccountToEdit(Account acc, IEnumerable<Group> groups)
        {
            this.account = acc;

            //this.groups = new System.Web.Mvc.SelectList(groups, acc.groupFK);
            this.groups = new System.Web.Mvc.SelectList( groups, "idGroup", "name");
        }
    }
}