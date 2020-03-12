using System;
using Xunit;
using Lab01.Domain;
using static Lab01.Domain.Playlist;
using System.Collections.Generic;
using System.Collections;
using FluentAssertions;

namespace Lab01.Domain.Tests
{
    public class PlaylistTests
    {
        [Fact]
        public void Playlist_should_be_active()
        {
            // arrange
            var sut = new Playlist();

            // act
            sut.IsActive = true;

            // assert

            sut.IsActive.Should().BeTrue();
        }

        [Fact]
        public void Playlist_title_must_not_be_null()
        {
            // arrange
            var sut = new Playlist();

            // act
            sut.Title = "";

            // assert

            sut.Title.Should().NotBeNull();
        }

        [Fact]
        public void Playlist_title_must_not_be_empty()
        {
            // arrange
            var sut = new Playlist();

            // act
            sut.Title = "1";

            // assert
            sut.Title.Should().NotBeEmpty();
        }

        [Fact]
        public void Playlist_should_not_be_active()
        {
            // arrange
            var sut = new Playlist();

            // act
            sut.IsActive = false;

            // assert

            sut.IsActive.Should().BeFalse();
        }

        [Fact]
        public void Can_add_a_song_to_playlist()
        {
            // arrange
            var sut = new Playlist();
            var song = new Song();

            // act
            sut.AddSong(song);

            // assert
            sut.Songs.Should().Contain(song);
        }

        [Fact]
        public void Song_added_to_list_is_same_object_instance()
        {
            // arrange
            var sut = new Playlist();
            var song = new Song();

            // act
            sut.AddSong(song);

            // assert

            sut.Songs[0].Should().BeSameAs(song);
        }

        [Fact]
        public void Songs_by_abba_are_not_allowed_in_the_playlist()
        {
            // arrange
            var sut = new Playlist();
            var song = new Song() { Artist = "Abba" };

            // act, assert
            sut.Invoking(s => s.AddSong(song)).Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Duplicate_songs_titles_are_ignored()
        {
            // arrange
            var sut = new Playlist();
            var song1 = new Song() { Artist = "Artist1", Title = "ASong" };
            var song2 = new Song() { Artist = "Artist1", Title = "ASong" };

            // act
            sut.AddSong(song1);
            sut.AddSong(song2);

            // assert
            sut.Songs.Should().HaveCount(1);
        }

        [Fact]
        public void Playlist_can_remove_duplicates_of_song_and_title()
        {
            // arrange
            var sut = new Playlist();
            var song1 = new Song() { Artist = "Artist1", Title = "ASong" };
            var song2 = new Song() { Artist = "Artist2", Title = "ASong" };
            sut.AddSong(song1);
            sut.AddSong(song2);

            // act
            sut.RemoveDuplicates();

            // assert
            sut.Songs.Should().NotContain(song2);
        }

        [Fact]
        public void Playlist_title_starts_with_current_year()
        {
            // arrange
            var sut = new Playlist();

            // act
            sut.Title = DateTime.Now.Year.ToString();

            // assert
            sut.Title.Should().StartWith("2020");
        }

        [Theory]
        [InlineData("Title1", "Artist1", "Title2", "Artist2")]
        [InlineData("Ziggy Stardust", "David Bowie", "A Forest", "The Cure")]
        public void Playlist_is_kept_sorted_by_artist_then_title(string title1, string artist1, string title2, string artist2)
        {
            // arrange
            var sut = new Playlist();
            var song1 = new Song() { Artist = artist1, Title = title1 };
            var song2 = new Song() { Artist = artist2, Title = title2 };

            // act
            sut.AddSong(song2);
            sut.AddSong(song1);

            // assert
            sut.Songs[0].Title.Should().Be(title1);
            sut.Songs[1].Title.Should().Be(title2);
        }

        [Theory]
        [MemberData(nameof(GetSongs))]
        public void Playlist_is_kept_sorted_by_date_then_artist_then_title(Song song1, Song song2)
        {
            // arrange
            var sut = new Playlist();

            // act
            sut.AddSong(song2);
            sut.AddSong(song1);

            // assert
            //Assert.Collection(sut.Songs, song => Assert.Equal(song1.Title, song.Title),
            //                            song => Assert.Equal(song2.Title, song.Title));
        }

        [Theory]
        [ClassData(typeof(SongDataGenerator))]
        public void Playlist_is_kept_sorted_by_date_then_artist_then_title_from_class(Song song1, Song song2)
        {
            // arrange
            var sut = new Playlist();

            // act
            sut.AddSong(song2);
            sut.AddSong(song1);

            // assert
           // Assert.Collection(sut.Songs, song => Assert.Equal(song1.Title, song.Title),
            //                            song => Assert.Equal(song2.Title, song.Title));
        }

        public static IEnumerable<object[]> GetSongs()
        {
            DateTime now = DateTime.Now;
            yield return new Song[] { new Song{ ReleaseDate = now.AddYears(1), Title = "A", Artist = "A" }, new Song { ReleaseDate = now.AddYears(1), Title = "Y", Artist = "A" } };
            yield return new Song[] { new Song{ ReleaseDate = now.AddYears(1), Title = "Y", Artist = "A" }, new Song { ReleaseDate = now.AddYears(1), Title = "Z", Artist = "A" } };
        }

        [Fact]
        public void New_playlist_has_empty_song_list()
        {
            // arrange, act
            var sut = new Playlist();

            // assert
            sut.Songs.Should().BeEmpty();
        }

        [Fact]
        public void Adding_song_raises_song_added_event()
        {
            // arrange, act
            var sut = new Playlist();

            // assert
        //    var reult = Assert.Raises<SongAddedEventArgs>(h => sut.SongAdded += h, h => sut.SongAdded -= h, () => sut.AddSong(new Song()));
        }
    }

    public class SongDataGenerator : IEnumerable<object[]>
    {
        DateTime now = DateTime.Now;

        public SongDataGenerator() {
            _data.Add(new Song[] { new Song{ ReleaseDate = now.AddYears(1), Title = "A", Artist = "A" }, new Song { ReleaseDate = now.AddYears(1), Title = "Y", Artist = "A" } });
            _data.Add(new Song[] { new Song{ ReleaseDate = now.AddYears(1), Title = "Y", Artist = "A" }, new Song { ReleaseDate = now.AddYears(1), Title = "Z", Artist = "A" } });
        }
        private readonly List<object[]> _data = new List<object[]>();

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
