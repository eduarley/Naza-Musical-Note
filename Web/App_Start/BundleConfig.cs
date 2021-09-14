using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));






            // Jquery UI
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                         "~/Scripts/jquery-ui-{version}.js"));
            //Jquery css  
            bundles.Add(new StyleBundle("~/Content/cssjqueryui").Include(
                         "~/Content/themes/base/*.css"));



            // Datatable js
            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                         "~/Content/plugins/datatables/jquery.dataTables.min.js",
                        "~/Content/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js",
                        "~/Content/plugins/datatables-responsive/js/dataTables.responsive.min.js",
                        "~/Content/plugins/datatables-responsive/js/responsive.bootstrap4.min.js",
                        "~/Content/plugins/datatables-buttons/js/dataTables.buttons.min.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.bootstrap4.min.js",
                        "~/Content/plugins/jszip/jszip.min.js",
                        "~/Content/plugins/pdfmake/pdfmake.min.js",
                        "~/Content/plugins/pdfmake/vfs_fonts.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.html5.min.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.print.min.js",
                        "~/Content/plugins/datatables-buttons/js/buttons.colVis.min.js"));

            //Datatable css
            bundles.Add(new StyleBundle("~/Content/datatable").Include(
                      "~/Content/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                      "~/Content/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                      "~/Content/plugins/datatables-buttons/css/buttons.bootstrap4.min.css"));

            //AdminLTE y Jquery js
            bundles.Add(new ScriptBundle("~/bundles/adminlte").Include(
                        "~/Content/plugins/jquery/jquery.min.js",
                         "~/Content/plugins/jquery/jquery-ui.min.js",
                        "~/Content/plugins/bootstrap/js/bootstrap.bundle.min.js",
                        //Adminlte
                        "~/Content/dist/js/adminlte.js",
                        "~/Content/dist/js/demo.js",
                        "~/Content/",
                        "~/Content/",
                        "~/Content/",
                        "~/Content/"));


            //AdminLTE css
            bundles.Add(new StyleBundle("~/Content/adminlte").Include(
                      "~/Content/plugins/fontawesome-free/css/all.min.css",
                      "~/Content/dist/css/adminlte.min.css",
                      "~/Content/",
                      "~/Content/",
                      "~/Content/"));





            // SweetAlert js
            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include(
                         "~/Content/plugins/sweetalert2/sweetalert2.min.js"));

            //SweetAlert css
            bundles.Add(new StyleBundle("~/Content/sweetalert").Include(
                      "~/Content/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css"));
        }
    }
}
