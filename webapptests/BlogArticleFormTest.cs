using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Security;
using NUnit.Framework;
using webapp;
using webapp.blog;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidationWhenCreatingEmptyForm()
        {
            BlogArticleForm form = new BlogArticleForm();
            Assert.IsFalse(form.Validate());
            var errors = form.Errors;
            Assert.AreEqual(2, errors.Count);
            Assert.Contains(NotEmptyAttribute.ErrorString, errors["Title"]);
            Assert.Contains(NotEmptyAttribute.ErrorString, errors["Content"]);

            form.Title = "";
            form.Content = "";
            Assert.IsFalse(form.Validate());

            errors = form.Errors;
            Assert.AreEqual(2, errors.Count);
            Assert.Contains(NotEmptyAttribute.ErrorString, errors["Title"]);
            Assert.Contains(NotEmptyAttribute.ErrorString, errors["Content"]);

            form.Title = "Hello";
            form.Content = "World";
            Assert.IsTrue(form.Validate());
            errors = form.Errors;
            Assert.AreEqual(0, errors.Count);
        }
        
        [Test]
        public void CreatingFormFromNameValueCollection()
        {
            NameValueCollection collection0 = new NameValueCollection();
            collection0.Add("Title", "");
            collection0.Add("Content", "");
            BlogArticleForm form0 = new BlogArticleForm(collection0);
            Assert.AreEqual("", form0.Title);
            Assert.AreEqual("", form0.Content);
            Assert.IsFalse(form0.Validate());
            Assert.AreEqual(2, form0.Errors.Count);
            Assert.Contains(NotEmptyAttribute.ErrorString, form0.Errors["Title"]);
            Assert.Contains(NotEmptyAttribute.ErrorString, form0.Errors["Content"]);

            NameValueCollection collection1 = new NameValueCollection();
            collection1.Add("Title", "Title1");
            collection1.Add("Content", "Content1");
            BlogArticleForm form1 = new BlogArticleForm(collection1);
            Assert.AreEqual("Title1", form1.Title);
            Assert.AreEqual("Content1", form1.Content);
            Assert.IsTrue(form1.Validate());
            Assert.AreEqual(0, form1.Errors.Count);
        }
    }
}