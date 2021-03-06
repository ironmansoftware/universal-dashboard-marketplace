﻿using Flurl;
using HtmlAgilityPack;
using System;
using System.Net;

namespace Marketplace.Services
{
    public class GitHubService
    {
        public bool ValidateGitHubUrl(string projectUri)
        {
            if (Uri.TryCreate(projectUri, UriKind.Absolute, out Uri result))
            {
                if (!result.Host.Equals("github.com"))
                {
                    return false;
                }

                return result.LocalPath.Split("/").Length == 2;
            }

            return false;
        }

        public string GetAccountName(string projectUri)
        {
            if (Uri.TryCreate(projectUri, UriKind.Absolute, out Uri result))
            {
                var parts = result.LocalPath.Split("/");

                return parts[0];
            }

            throw new Exception("Invalid URI");
        }

        public string GetProjectName(string projectUri)
        {
            if (Uri.TryCreate(projectUri, UriKind.Absolute, out Uri result))
            {
                var parts = result.LocalPath.Split("/");

                return parts[1];
            }

            throw new Exception("Invalid URI");
        }

        public string GetReadme(string projectUri)
        {
            try
            {
                if (Uri.TryCreate(projectUri, UriKind.Absolute, out Uri result))
                {
                    var uriBuilder = new UriBuilder();
                    uriBuilder.Scheme = "https";
                    uriBuilder.Host = "raw.githubusercontent.com";
                    uriBuilder.Path = Url.Combine(result.LocalPath, "master", "README.md");

                    var wc = new WebClient();
                    var readme = wc.DownloadString(uriBuilder.Uri);

                    var html = CommonMark.CommonMarkConverter.Convert(readme);

                    var doc = new HtmlDocument();
                    doc.LoadHtml(html);

                    var images = doc.DocumentNode.Descendants("img");
                    foreach(var img in images)
                    {
                        if (img.Attributes.Contains("src"))
                        {
                            img.Attributes["src"].Value = ReplaceLocalUrl(img.Attributes["src"].Value, result);
                        }
                    }

                    return doc.DocumentNode.OuterHtml;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        private string ReplaceLocalUrl(string localUrl, Uri projectUri)
        {
            if (Uri.IsWellFormedUriString(localUrl, UriKind.Absolute))
            {
                return localUrl;
            }

            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = "raw.githubusercontent.com";
            uriBuilder.Path = Url.Combine(projectUri.LocalPath, "master", localUrl);

            return uriBuilder.Uri.ToString();
        }
    }
}
