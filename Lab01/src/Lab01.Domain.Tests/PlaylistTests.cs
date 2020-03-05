using System;
using Xunit;
using Lab01.Domain;
using System.Linq;

namespace Lab01.Domain.Tests
{
    public class PlaylistTests
    {
        [Fact]
        public void Test1()
        {
            var playlist = new Playlist(string.Empty);

            Assert.NotNull(playlist.Title);
        }

        [Fact]
        public void Test2()
        {
            var playlist = new Playlist(string.Empty);

            Assert.True(playlist.IsActive);
        }

        [Fact]
        public void Test3()
        {
            var playlist = new Playlist(string.Empty);

            Assert.NotEmpty(playlist.Title);
        }

        [Fact]
        public void Test4()
        {
            var playlist = new Playlist(string.Empty);

            playlist.Songs.Add(new Playlist.Song());

            Assert.True(playlist.Songs.Count() > 0);
        }
    }
}
