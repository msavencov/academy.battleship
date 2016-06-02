using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Academy.BattleShip.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var css = new StyleBundle("~/content/style-bundle");
            css.Include("~/bower_components/bootstrap/dist/css/bootstrap.min.css");
            css.Include("~/bower_components/bootstrap/dist/css/bootstrap-theme.min.css");

            var js = new ScriptBundle("~/content/script-bundle");
            js.Include("~/bower_components/jquery/dist/jquery.min.js");
            js.Include("~/bower_components/bootstrap/dist/js/bootstrap.min.js");

            bundles.Add(css);
            bundles.Add(js);


        }
    }
}