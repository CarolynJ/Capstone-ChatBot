using StudentChatBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChatBot.DAL
{
    public interface ISearchByKeyword
    {
        List<Resource> GetResources(string keyword);
    }
}
