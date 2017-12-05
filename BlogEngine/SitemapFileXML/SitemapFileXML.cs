using BlogEngine.Core.Web.Extensions;
using System;
using System.IO;
using System.Web.Hosting;
using System.Collections.Generic;
using BlogEngine.Core;
using BlogEngine.Core.Web.Controls;
using System.Web;
using System.Linq;
using System.Globalization;
using System.Xml;

namespace App_Code.Extensions
{


    /// <summary>
    /// After the release of the blog automatically generate a static xml sitemap file.
    /// Many search engines do not support the axd type address .
    /// </summary>
    [Extension("After the release of the blog automatically generate a static xml sitemap file.Many search engines do not support the axd type address .", "1.0", "skyfinder", 0, false)]
    public class SitemapFileXML
    {
        #region Constants and Fields

        private const string BaseFilename = "sitemap.xml";

        /// <summary>
        /// The sync root.
        /// </summary>
        private static readonly object SyncRoot = new object();


        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="SitemapFileXML"/> class.
        /// </summary>
        static SitemapFileXML()
        {
            Post.Saved+= OnWriter;
        }

        #endregion



        #region Methods

        /// <summary>
        /// This event generates a static sitemap.xml file at the root of the program.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The event arguments.
        /// </param>
        private static void OnWriter(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }
            if (!ExtensionManager.ExtensionEnabled("SitemapFileXML"))
            {
                return;
            }
           var file = HostingEnvironment.ApplicationPhysicalPath+"\\"+BaseFilename;
            lock (SyncRoot)
            {
                try
                {
                    FileInfo fi = new FileInfo(file);
                    fi.Delete();

                    using (var writer = XmlWriter.Create(file))
                    {
                        writer.WriteStartElement("urlset", BlogConfig.SiteMapUrlSet);

                        // Posts
                        foreach (var post in Post.Posts.Where(post => post.IsVisibleToPublic))
                        {
                            writer.WriteStartElement("url");
                            writer.WriteElementString("loc", post.AbsoluteLink.AbsoluteUri.ToString());
                            writer.WriteElementString(
                                "lastmod", post.DateModified.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                            writer.WriteElementString("changefreq", "monthly");
                            writer.WriteEndElement();
                        }

                        // Pages
                        foreach (var page in Page.Pages.Where(page => page.IsVisibleToPublic))
                        {
                            writer.WriteStartElement("url");
                            writer.WriteElementString("loc", page.AbsoluteLink.AbsoluteUri);
                            writer.WriteElementString(
                                "lastmod", page.DateModified.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                            writer.WriteElementString("changefreq", "monthly");
                            writer.WriteEndElement();
                        }

                        // Removed for SEO reasons
                        //// Archive
                        // writer.WriteStartElement("url");
                        // writer.WriteElementString("loc", Utils.AbsoluteWebRoot.ToString() + "archive.aspx");
                        // writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));
                        // writer.WriteElementString("changefreq", "daily");
                        // writer.WriteEndElement();

                        // Contact
                        writer.WriteStartElement("url");
                        writer.WriteElementString("loc", $"{Utils.AbsoluteWebRoot}contact.aspx");
                        writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                        writer.WriteElementString("changefreq", "monthly");
                        writer.WriteEndElement();

                        // Blog
                        if (Page.GetFrontPage() != null)
                        {
                            writer.WriteStartElement("url");
                            writer.WriteElementString("loc", $"{Utils.AbsoluteWebRoot}blog.aspx");
                            writer.WriteElementString(
                                "lastmod", DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                            writer.WriteElementString("changefreq", "daily");
                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                    }
                }
                catch(Exception ex)
                {
                    // Absorb the error.
                    Utils.Log($"Error write xml website SitemapFileXML: {ex.Message}");
                }
            }
        }

        #endregion
    }
}