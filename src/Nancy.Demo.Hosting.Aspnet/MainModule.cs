namespace Nancy.Demo.Hosting.Aspnet
{
    using System;
    using Nancy.Demo.Hosting.Aspnet.Models;
    using Nancy.Routing;

    public class MainModule : NancyModule
    {
        public MainModule(IRouteCacheProvider routeCacheProvider)
        {
            Get["/"] = x => {
                return View["routes.cshtml", routeCacheProvider.GetCache()];
            };

            Get["/style/{file}"] = x => {
                return Response.AsCss("Content/" + (string)x.file);
            };

            Get["/scripts/{file}"] = x => {
                return Response.AsJs("Content/" + (string)x.file);
            };

            Get["/images/{file}"] = x =>
            {
                return Response.AsImage("Content/" + (string)x.file);
            };

            Get["/filtered", r => true] = x => {
                return "This is a route with a filter that always returns true.";
            };

            Get["/filtered", r => false] = x => {
                return "This is also a route, but filtered out so should never be hit.";
            };

            Get[@"/(?<foo>\d{2,4})/{bar}"] = x => {
                return string.Format("foo: {0}<br/>bar: {1}", x.foo, x.bar);
            };

            Get["/test"] = x => {
                return "Test";
            };

            Get["/dotliquid"] = parameters => {
                return View["dot", new { name = "dot" }];
            };

            Get["/javascript"] = x => {
                return View["javascript.html"];
            };

            Get["/static"] = x => {
                return View["static.htm"];
            };

            Get["/razor"] = x => {
                var model = new RatPack { FirstName = "Frank" };
                return View["razor.cshtml", model];
            };

            Get["/razor-simple"] = x =>
            {
                var model = new RatPack { FirstName = "Frank" };
                return View["razor-simple.cshtml", model];
            };

            Get["/razor-dynamic"] = x =>
            {
                return View["razor.cshtml", new { FirstName = "Frank" }];
            };

            Get["/ssve"] = x =>
            {
                var model = new RatPack { FirstName = "You" };
                return View["ssve.sshtml", model];
            };

            Get["/viewmodelconvention"] = x => {
                return View[new SomeViewModel()];
            };

            Get["/ndjango"] = x => {
                var model = new RatPack { FirstName = "Michael" };
                return View["ndjango.django", model];
            };

            Get["/ndjango-extends"] = x => {
                var model = new RatPack { FirstName = "Michael" };
                return View["with-master.django", model];
            };

            Get["/spark"] = x => {
                var model = new RatPack { FirstName = "Bright" };
                return View["spark.spark", model];
            };

            Get["/spark-anon"] = x =>
            {
                var model = new { FirstName = "Anonymous" };
                return View["anon.spark", model];
            };

            Get["/json"] = x => {
                var model = new RatPack { FirstName = "Andy" };
                return Response.AsJson(model);
            };

            Get["/xml"] = x => {
                var model = new RatPack { FirstName = "Andy" };
                return Response.AsXml(model);
            };

            Get["/session"] = x => {
                var value = Session["moo"] ?? "";

                var output = "Current session value is: " + value;

                if (String.IsNullOrEmpty(value.ToString()))
                {
                    Session["moo"] = "I've created a session!";
                }

                return output;
            };

            Get["/sessionObject"] = x => {
                var value = Session["baa"] ?? "null";

                var output = "Current session value is: " + value;

                if (value.ToString() == "null")
                {
                    Session["baa"] = new Payload(27, true, "some random string value");
                }

                return output;
            };

            Get["/error"] = x =>
                {
                    throw new NotSupportedException("This is an exception thrown in a route.");
                };
        }
    }
}
