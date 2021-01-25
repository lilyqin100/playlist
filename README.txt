1.  This program uses DOTNET Core 3.1 which is platform independent and the same working C# program works for Windows 10 (my PC),
    iOS and Linux platforms.

    To run the program on any platform, from the HighSpot.csproj file, I add these lines:
     <PropertyGroup>
       <RuntimeIdentifiers>win10-x64;osx.10.12-x64;debian.8-x64</RuntimeIdentifiers>
     </PropertyGroup> 

     This tells .NET that we want to build a self-contained version of our app for Windows 10 64-bit, macOS Sierra, and Debian 8.
     
     i) On a Windows 10 console, type    "file -b win10-x64/app.exe"
                             and then "dotnet publish win10-x64"
                             and then "dotnet build" and "dotnet run"
          This will produce the desired output.json file in the same directory as mixtap.json from the program.  I have tested this out  on two Windows 10 PCs.
     ii) On a iOs system console, type " file -b osx.10.12-x64/app"
                               and then "dotnet publish osx.10.12-x64"
                             and then "dotnet build" and "dotnet run"
          This should produce the desired output.json file in the same directory as mixtap.json from the program. I don't have a suitable iOS device to test this out yet.
     iii) On a Linux system console, type " file -b debian.8-x64/app"
                                 and then "dotnet publish debian.8-x64"
                                 and then "dotnet build" and "dotnet run"
          This should produce the desired output.json file in the same directory as mixtap.json from the program. I don't have a suitable Linux device to test this out yet.


     Reference article: https://opensource.com/article/17/5/cross-platform-console-apps

2. I chose the change file to be in JSON format as well which satisfies the three requirements as my coding sample change file, namely changes.json
    i) Add a new playlist
    ii) Remove a playlist
    iii) Add an eixsting song to an existing playlist
    In the program, I assume these file changes inputs are valid inputs, otherwise I'd throw general error, although these error handlings can be
    more refined, depending on the refined requirements such as the new playlist to be added has duplicate playlist "id" as an existing playlist,
    or the song to be added to a playlist has duplicate song "id" as the existing song list.

3. The program can be easily extended to handle forseable use scenarios, such as add multiple new playlists, remove multiple playlists, or add multiple
   existing sings to an existing playlist. If this needs t be doe, the changes.json format just needs to handle  array format, and the three main methods:
      AddPlayList( ...)
      RemovePlayList(...)
      AddSongToPlayList(...)
  currently what is implemented in Porgram.cs can be reused in for loops for the matter

4. Unit Tests:
   The above three methods can also be refactored into a public class to be called from Main().  It can be implemented in a more object-oriented manner,
   with unit tests to be deveoped first, which will verify these methods with different changes set data inputs, corner cases, more complex changes, 
   error handling requirements such as duplicate changes, or changes  duplicate the existing playlist ID, blab blah... All of those can be further developed
   and extended with a nice unit tests coverage to insure each method's functionality...
 
5. Scalability:
   i. 
   In the current implementation, it would assume the input mixtape.json and the changes.json are relatively small.  That is, after the deserialization,
   the input data can fit into object models in memory. However, if one or both of these input JSON files are very large, and can not fit into memory, we'd 
   need different approaches to deserialDOTize the JSON file or JSON data stream.  Fortunately, this performance has been taken into consideration by
   the new .DOTNET Core System.Text.Json library, which performs better than the DOTNET NewtonSoft JSON parser.  Namely, we can use JsonDocument.Parse to
   deserializes JSON to its constituent parts and makes it accessible through an object model that can represent any valid JSON. The object model gives you 
   the power to read arbitrary parts of the JSON document.

   Utf8JsonReader can be another parser whch can work with large input JSON files which will minimize the memeory allocation, and to have developers to access
   or deserialize only needed data values.

   Reference:  https://marcroussy.com/2020/08/17/deserialization-with-system-text-json/

   We can use the more performant JsonDocument or Utf8JsonReader to handle the large input JSON files in .NET Core, as referenced above.  For example,
   if we have a fairly large playlists input to be added to the mixtape.json, we can process the changes file in chucks, and add them to an output data streams
   in chucks.  

   ii.
    I'd like to mention here is that actually we can optimize the three main methods from linear scan O(N) to be in O( log N)
      AddPlayList( ...)
      RemovePlayList(...)
      AddSongToPlayList(...)
 Currently to find a matching playlist ID, or a matching song id, I am using linear scans.  This can be optimized if we order these primary IDs in the
 "users", "playlists" and the "songs" tables.  We can basically store the mixtape.json input data into a relational database, normalize them by their primary keys,
  namely, "id"s, then the linear scan can be optimized in a binary searh manner that it would only take O(log N) time to find the matching IDs. This will greatly
   increase the performance of these three main methods. Also the large changes.json can be processed in chucks and we just need to update the relational
    database tables multiple times until all data sets in changes.json is processed.  THis would be App database disk processing vs. app processing in memory.
   But an O(log N) methods would trade off for better in this approach.



  

        

 
    
