using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace WebScrapping.SoccorRepository
{
    public class SoccorRepoBLL
    {
        private static string WebApp = ConfigurationManager.ConnectionStrings["UserWebApp"].ConnectionString;
        public static bool InsertAll(Soccor soccor)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(WebApp))
                {
                    string sp_name = "usp_InsertSoccor";
                    var param = new DynamicParameters();
                    param.Add("@MatchId", soccor.MatchId);
                    param.Add("@MatchTime", soccor.MatchTime);
                    param.Add("@HomeId", soccor.HomeId);
                    param.Add("@HomeName", soccor.HomeName);
                    param.Add("@AwayId", soccor.AwayId);
                    param.Add("@AwayName", soccor.AwayName);
                    param.Add("@ContestGroupName", soccor.ContestGroupName);
                    //param.Add("@HomeLink", soccor.HomeLink);
                    //param.Add("@AwayLink", soccor.AwayLink);
                    sqlCon.Query<Soccor>(sp_name, param, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception e)
            {
                var ex = e.Message;
            }
            return true;
        }
    }
}
