# lyrical App
A little console app to get average number of words for an artists tracks.

### Requirements
- C# 6
- Visual Studio 2022

### Setup 
- Enter Spotify API client settings in appSettings.json (provided separately)
- Make sure the Lyrical project is set as the startup project
- Start Debugging to run!

### External Resources
- [Spotify API using SpotifyAPI-NET](https://johnnycrazy.github.io/SpotifyAPI-NET/) used to get track list
- [Lyrics.ovh](https://lyricsovh.docs.apiary.io/#reference) use for lyrics


### Limitations
- To get a list of artist's songs it first has to get the artist by searching by name - this could return multiple results but the app will just pick the first.
- Once the artist is found it will then search for a list of albums with results paged but the app currently only picks the first batch.
- As above but with track search.
- Lyric results are limited to the first 20 as the API has a tendancy to start slowing down / timing out, API also often doesn't return results for valid track names.