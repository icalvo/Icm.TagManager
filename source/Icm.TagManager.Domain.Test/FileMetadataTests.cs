using System;
using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Icm.TagManager.Domain.Test
{
    public class FileMetadataTests
    {
        #region Ctor

        [Fact]
        public void Ctor1_PathNull_ThrowsArgumentNullException()
        {
            Action action = () => { var metadata = new FileMetadata(null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Ctor2_PathNull_ThrowsArgumentNullException()
        {
            Action action = () => { var metadata = new FileMetadata(null, "tag"); };
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Ctor2_TagsNull_ThrowsArgumentNullException()
        {
            Action action = () => { var metadata = new FileMetadata("a", (string[]) null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Ctor3_PathNull_ThrowsArgumentNullException()
        {
            Action action = () => { var metadata = new FileMetadata(null, new List<string> {"tag"}); };
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Ctor3_TagsNull_ThrowsArgumentNullException()
        {
            Action action = () => { var metadata = new FileMetadata("a", (IEnumerable<string>) null); };
            action.ShouldThrow<ArgumentNullException>();
        }

        #endregion

        [Fact]
        public void AddTag_WhenNull_ThrowsArgumentNullException()
        {
            var metadata = new FileMetadata("a");
            Action action = () =>
            {
                metadata.AddTag(null);
            };
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void AddTag_WhenEmpty_AddsTag()
        {
            var metadata = new FileMetadata("a");

            metadata.AddTag("tag1");

            metadata.Tags.Should().BeEquivalentTo("tag1");
        }

        [Fact]
        public void AddTag_WhenDifferentTagsExist_AddsTag()
        {
            var metadata = new FileMetadata("a");
            metadata.AddTag("tag1");

            metadata.AddTag("tag2");

            metadata.Tags.Should().BeEquivalentTo("tag1", "tag2");
        }

        [Fact]
        public void AddTag_WhenSameTagExists_KeepsTags()
        {
            var metadata = new FileMetadata("a");
            metadata.AddTag("tag1");

            metadata.AddTag("tag1");

            metadata.Tags.Should().BeEquivalentTo("tag1");
        }

        [Fact]
        public void AddTag_WhenCasedTag_ConvertsToLower()
        {
            var metadata = new FileMetadata("a");

            metadata.AddTag("TAG1");

            metadata.Tags.Should().BeEquivalentTo("tag1");
        }
    }
}