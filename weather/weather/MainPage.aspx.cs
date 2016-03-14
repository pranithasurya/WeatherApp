using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using weather.DataAccessLayer;
using System.Web.Http;
using System.Data;
using weather.util;

namespace weather
{

    public partial class MainPage : System.Web.UI.Page
    {
        DataAccessObject dao = new DataAccessObject();
        protected void Page_Load(object sender, EventArgs e)
        {
            dao.readData();
            if (!IsPostBack)
            {

                Tab1.CssClass = "Clicked";
                MainView.ActiveViewIndex = 0;
                //dao.readData();
                string[] allCountries = dao.countryToCityMap.Keys.ToArray();
                totalcountries.DataSource = allCountries;
                totalcountries.DataBind();
            }
        }
        protected void Tab1_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Clicked";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Initial";
            MainView.ActiveViewIndex = 0;
        }

        protected void Tab2_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Clicked";
            Tab3.CssClass = "Initial";
            MainView.ActiveViewIndex = 1;
        }

        protected void Tab3_Click(object sender, EventArgs e)
        {
            Tab1.CssClass = "Initial";
            Tab2.CssClass = "Initial";
            Tab3.CssClass = "Clicked";
            MainView.ActiveViewIndex = 2;
        }


        protected void getAverageTemparature(object sender, EventArgs e)
        {
           // dao.readData();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add("Country");
            dt.Columns.Add("Temperature");


            List<ListItem> items = totalcountries.Items.OfType<ListItem>().Where(i => i.Selected).ToList();
            foreach (ListItem listItem in items)
            {
                dr = dt.NewRow();

                float temp = dao.getCountryTemperature(listItem.Value);
                dr["Country"] = listItem.Value;
                dr["Temperature"] = temp;
                dt.Rows.Add(dr);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        protected void searchCities(object sender, EventArgs e)
        {
           // dao.readData();
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add("Country");
            dt.Columns.Add("CityCount");

            int startTemp = Int32.Parse(from.Text);
            int toTemp = Int32.Parse(to.Text);
            List<resultsPrb2> res = dao.getCityCountForTempRange(startTemp, toTemp);
            foreach (resultsPrb2 listItem in res)
            {
                dr = dt.NewRow();

                dr["Country"] = listItem.country;
                dr["CityCount"] = listItem.count;
                dt.Rows.Add(dr);
            }
            GridView2.DataSource = dt;
            GridView2.DataBind();

        }
       
    }
}