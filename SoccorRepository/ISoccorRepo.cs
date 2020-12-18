using System;
using System.Collections.Generic;
using System.Text;

namespace WebScrapping.SoccorRepository
{
    public interface ISoccorRepo
    {
        bool InsertAll(Soccor soccor);
    }
}
