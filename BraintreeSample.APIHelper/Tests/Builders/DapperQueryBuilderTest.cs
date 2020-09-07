using BraintreeSample.APIHelper.Builders;
using BraintreeSample.APIHelper.Entities;
using NUnit.Framework;
using System;

namespace BraintreeSample.APIHelper.Tests.Builders
{
    public class DapperQueryBuilderTest
    {
        internal class SampleEntity : BaseEntity
        {
            public int sampleInt { get; set; }
            public string sampleStr { get; set; }
            public bool sampleBool { get; set; }
            public DateTime sampleDateTime { get; set; }
        }
        //[Test]
        public void CreateQueryShouldReturnInsertIntoStatement()
        {
            // Given
            var entity = new SampleEntity()
            {
                sampleInt = 1,
                sampleStr = "str",
                sampleBool = true,
                sampleDateTime = DateTime.Now
            };
            var dapperQueryBuilder = new DapperQueryBuilder<SampleEntity>();

            // When
            var query = dapperQueryBuilder.CreateQuery(entity);

            // Then
            Assert.AreEqual($"insert into samples(sampleint, samplestr, samplebool, sampledatetime, guid, isactive, isdeleted, createdat, createduserid, updatedat, updateduserid) values(1, 'str', True, '{DateTime.Now}', False, False, '{DateTime.Now}', 0, '{DateTime.Now}', 0) returning id", query);
        }
        [Test]
        public void ReadQueryWithIdShouldReturnInsertIntoStatement()
        {
            // Given
            var id = 1;
            var dapperQueryBuilder = new DapperQueryBuilder<SampleEntity>();

            // When
            var query = dapperQueryBuilder.ReadQuery(id);

            // Then
            Assert.AreEqual($"select * from samples where id=1", query);
        }
        [Test]
        public void ReadQueryWithGuidShouldReturnInsertIntoStatement()
        {
            // Given
            var guid = new Guid("2d62fa93-1be2-4e71-a8df-531b2fc278c3");
            var dapperQueryBuilder = new DapperQueryBuilder<SampleEntity>();

            // When
            var query = dapperQueryBuilder.ReadQuery(guid);

            // Then
            Assert.AreEqual($"select * from samples where guid='2d62fa93-1be2-4e71-a8df-531b2fc278c3'", query);
        }
        [Test]
        public void UpdateQueryShouldReturnInsertIntoStatement()
        {
            // Given
            var guid = "2d62fa93-1be2-4e71-a8df-531b2fc278c3";
            var now = DateTime.Now;
            var entity = new SampleEntity()
            {
                ID = 1,
                Guid = guid,
                sampleInt = 2,
                sampleStr = "str",
                sampleBool = true,
                sampleDateTime = now
            };
            var dapperQueryBuilder = new DapperQueryBuilder<SampleEntity>();

            // When
            var query = dapperQueryBuilder.UpdateQuery(entity);

            // Then
            Assert.AreEqual($"update samples set sampleint=2, samplestr='str', samplebool=True, sampledatetime='{DateTime.Now}', isactive=False, isdeleted=False, updatedat='{now}' where id=1 || guid='{guid}'", query);
        }
        [Test]
        public void DeleteQueryWithIdShouldReturnInsertIntoStatement()
        {
            // Given
            var id = 1;
            var dapperQueryBuilder = new DapperQueryBuilder<SampleEntity>();

            // When
            var query = dapperQueryBuilder.DeleteQuery(id);

            // Then
            Assert.AreEqual($"update samples set isdeleted=true, isactive=false where id=1", query);
        }
        [Test]
        public void DeleteQueryWithGuidShouldReturnInsertIntoStatement()
        {
            // Given
            var guid = new Guid("2d62fa93-1be2-4e71-a8df-531b2fc278c3");
            var dapperQueryBuilder = new DapperQueryBuilder<SampleEntity>();

            // When
            var query = dapperQueryBuilder.DeleteQuery(guid);

            // Then
            Assert.AreEqual($"update samples set isdeleted=true, isactive=false where guid='2d62fa93-1be2-4e71-a8df-531b2fc278c3'", query);
        }
        [Test]
        public void ListQueryShouldReturnInsertIntoStatement()
        {
            // Given
            var page = 0;
            var pageSize = 3;
            var dapperQueryBuilder = new DapperQueryBuilder<SampleEntity>();

            // When
            var query = dapperQueryBuilder.ListQuery(page, pageSize);

            // Then
            Assert.AreEqual($"select * from samples order by id limit 3 offset 0", query);
        }
    }
}
