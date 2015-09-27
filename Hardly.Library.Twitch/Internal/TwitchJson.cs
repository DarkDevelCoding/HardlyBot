﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Hardly.Library.Twitch
{
    internal static class TwitchJson
    {
        internal static SqlTwitchUser ParseUser(string json)
        {
            // json = "_id":79242000,"name":"volt_100","created_at":"2015-01-08T17:08:01Z","updated_at":"2015-06-20T16:41:10Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100"},"display_name":"volt_100","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/volt_100-profile_image-653aa9b8c5343c09-300x300.jpeg","bio":null

            uint id;
            if (uint.TryParse(json.GetBetween("\"_id\":", ","), out id))
            {
                string name = json.GetBetween("\"display_name\":\"", "\"");
                DateTime created = default(DateTime);
                DateTime.TryParse(json.GetBetween("\"created_at\":\"", "\""), out created);
                string logo = json.GetBetween("\"logo\":\"", "\"");
                string bio = json.GetBetween("\"bio\":\"", "\"");

                return new SqlTwitchUser(id, name, created, logo, bio);
            }
            else
            {
                return null;
            }
        }

        internal static SqlTwitchFollower[] ParseFollowers(string json)
        {
            LinkedList<SqlTwitchFollower> followers = new LinkedList<SqlTwitchFollower>();

            if (json != null)
            {
                // {"follows":[{"created_at":"2015-06-27T06:25:12Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100/follows/channels/hardlysober"},"notifications":false,"user":{"_id":79242000,"name":"volt_100","created_at":"2015-01-08T17:08:01Z","updated_at":"2015-06-20T16:41:10Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100"},"display_name":"volt_100","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/volt_100-profile_image-653aa9b8c5343c09-300x300.jpeg","bio":null,"type":"user"}},{"created_at":"2015-06-27T05:50:23Z","_links":{"self":"https://api.twitch.tv/kraken/users/doctordoofenshmirtz/follows/channels/hardlysober"},"notifications":false,"user":{"_id":73136919,"name":"doctordoofenshmirtz","created_at":"2014-10-15T22:50:12Z","updated_at":"2015-06-20T01:00:20Z","_links":{"self":"https://api.twitch.tv/kraken/users/doctordoofenshmirtz"},"display_name":"DoctorDoofenshmirtz","logo":null,"bio":null,"type":"user"}},{"created_at":"2015-06-27T01:12:13Z","_links":{"self":"https://api.twitch.tv/kraken/users/dylanplayscc/follows/channels/hardlysober"},"notifications":false,"user":{"_id":94456844,"name":"dylanplayscc","created_at":"2015-06-25T19:18:22Z","updated_at":"2015-06-25T19:18:36Z","_links":{"self":"https://api.twitch.tv/kraken/users/dylanplayscc"},"display_name":"Dylanplayscc","logo":null,"bio":null,"type":"user"}},{"created_at":"2015-06-26T06:22:56Z","_links":{"self":"https://api.twitch.tv/kraken/users/goldguy421/follows/channels/hardlysober"},"notifications":false,"user":{"_id":92309564,"name":"goldguy421","created_at":"2015-05-30T23:01:27Z","updated_at":"2015-06-22T04:30:08Z","_links":{"self":"https://api.twitch.tv/kraken/users/goldguy421"},"display_name":"Goldguy421","logo":null,"bio":null,"type":"user"}},{"created_at":"2015-06-26T04:40:18Z","_links":{"self":"https://api.twitch.tv/kraken/users/mrbobcat321/follows/channels/hardlysober"},"notifications":false,"user":{"_id":92797431,"name":"mrbobcat321","created_at":"2015-06-06T01:53:50Z","updated_at":"2015-06-25T06:23:02Z","_links":{"self":"https://api.twitch.tv/kraken/users/mrbobcat321"},"display_name":"mrbobcat321","logo":null,"bio":"im 13 love to play wizard101 i going to start doing this more often so ya :)","type":"user"}}],"_total":437,"_links":{"self":"https://api.twitch.tv/kraken/channels/hardlysober/follows?direction=DESC&limit=5&offset=0","next":"https://api.twitch.tv/kraken/channels/hardlysober/follows?direction=DESC&limit=5&offset=5"}}

                json = json.GetBetween("{", "}", true, false);
                // "follows":[{"created_at":"2015-06-27T06:25:12Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100/follows/channels/hardlysober"},"notifications":false,"user":{"_id":79242000,"name":"volt_100","created_at":"2015-01-08T17:08:01Z","updated_at":"2015-06-20T16:41:10Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100"},"display_name":"volt_100","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/volt_100-profile_image-653aa9b8c5343c09-300x300.jpeg","bio":null,"type":"user"}},{"created_at":"2015-06-27T05:50:23Z","_links":{"self":"https://api.twitch.tv/kraken/users/doctordoofenshmirtz/follows/channels/hardlysober"},"notifications":false,"user":{"_id":73136919,"name":"doctordoofenshmirtz","created_at":"2014-10-15T22:50:12Z","updated_at":"2015-06-20T01:00:20Z","_links":{"self":"https://api.twitch.tv/kraken/users/doctordoofenshmirtz"},"display_name":"DoctorDoofenshmirtz","logo":null,"bio":null,"type":"user"}},{"created_at":"2015-06-27T01:12:13Z","_links":{"self":"https://api.twitch.tv/kraken/users/dylanplayscc/follows/channels/hardlysober"},"notifications":false,"user":{"_id":94456844,"name":"dylanplayscc","created_at":"2015-06-25T19:18:22Z","updated_at":"2015-06-25T19:18:36Z","_links":{"self":"https://api.twitch.tv/kraken/users/dylanplayscc"},"display_name":"Dylanplayscc","logo":null,"bio":null,"type":"user"}},{"created_at":"2015-06-26T06:22:56Z","_links":{"self":"https://api.twitch.tv/kraken/users/goldguy421/follows/channels/hardlysober"},"notifications":false,"user":{"_id":92309564,"name":"goldguy421","created_at":"2015-05-30T23:01:27Z","updated_at":"2015-06-22T04:30:08Z","_links":{"self":"https://api.twitch.tv/kraken/users/goldguy421"},"display_name":"Goldguy421","logo":null,"bio":null,"type":"user"}},{"created_at":"2015-06-26T04:40:18Z","_links":{"self":"https://api.twitch.tv/kraken/users/mrbobcat321/follows/channels/hardlysober"},"notifications":false,"user":{"_id":92797431,"name":"mrbobcat321","created_at":"2015-06-06T01:53:50Z","updated_at":"2015-06-25T06:23:02Z","_links":{"self":"https://api.twitch.tv/kraken/users/mrbobcat321"},"display_name":"mrbobcat321","logo":null,"bio":"im 13 love to play wizard101 i going to start doing this more often so ya :)","type":"user"}}],"_total":437,"_links":{"self":"https://api.twitch.tv/kraken/channels/hardlysober/follows?direction=DESC&limit=5&offset=0","next":"https://api.twitch.tv/kraken/channels/hardlysober/follows?direction=DESC&limit=5&offset=5"}

                SqlTwitchChannel channel = new SqlTwitchChannel(Twitch.GetUserFromName(json.GetBetween("https://api.twitch.tv/kraken/channels/", "/")));

                string follows = json.GetBetween("\"follows\":[", "]", true, false);
                // follows = {"created_at":"2015-06-27T06:25:12Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100/follows/channels/hardlysober"},"notifications":false,"user":{"_id":79242000,"name":"volt_100","created_at":"2015-01-08T17:08:01Z","updated_at":"2015-06-20T16:41:10Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100"},"display_name":"volt_100","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/volt_100-profile_image-653aa9b8c5343c09-300x300.jpeg","bio":null,"type":"user"}},{"created_at":"2015-06-27T05:50:23Z","_links":{"self":"https://api.twitch.tv/kraken/users/doctordoofenshmirtz/follows/channels/hardlysober"},"notifications":false,"user":{"_id":73136919,"name":"doctordoofenshmirtz","created_at":"2014-10-15T22:50:12Z","updated_at":"2015-06-20T01:00:20Z","_links":{"self":"https://api.twitch.tv/kraken/users/doctordoofenshmirtz"},"display_name":"DoctorDoofenshmirtz","logo":null,"bio":null,"type":"user"}},{"created_at":"2015-06-27T01:12:13Z","_links":{"self":"https://api.twitch.tv/kraken/users/dylanplayscc/follows/channels/hardlysober"},"notifications":false,"user":{"_id":94456844,"name":"dylanplayscc","created_at":"2015-06-25T19:18:22Z","updated_at":"2015-06-25T19:18:36Z","_links":{"self":"https://api.twitch.tv/kraken/users/dylanplayscc"},"display_name":"Dylanplayscc","logo":null,"bio":null,"type":"user"}},{"created_at":"2015-06-26T06:22:56Z","_links":{"self":"https://api.twitch.tv/kraken/users/goldguy421/follows/channels/hardlysober"},"notifications":false,"user":{"_id":92309564,"name":"goldguy421","created_at":"2015-05-30T23:01:27Z","updated_at":"2015-06-22T04:30:08Z","_links":{"self":"https://api.twitch.tv/kraken/users/goldguy421"},"display_name":"Goldguy421","logo":null,"bio":null,"type":"user"}},{"created_at":"2015-06-26T04:40:18Z","_links":{"self":"https://api.twitch.tv/kraken/users/mrbobcat321/follows/channels/hardlysober"},"notifications":false,"user":{"_id":92797431,"name":"mrbobcat321","created_at":"2015-06-06T01:53:50Z","updated_at":"2015-06-25T06:23:02Z","_links":{"self":"https://api.twitch.tv/kraken/users/mrbobcat321"},"display_name":"mrbobcat321","logo":null,"bio":"im 13 love to play wizard101 i going to start doing this more often so ya :)","type":"user"}}

                if (follows != null)
                {
                    string[] followsJsonList = follows.Tokenize("{\"created_at\":\"", true);
                    // followsList[i] = {"created_at":"2015-06-27T06:25:12Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100/follows/channels/hardlysober"},"notifications":false,"user":{"_id":79242000,"name":"volt_100","created_at":"2015-01-08T17:08:01Z","updated_at":"2015-06-20T16:41:10Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100"},"display_name":"volt_100","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/volt_100-profile_image-653aa9b8c5343c09-300x300.jpeg","bio":null,"type":"user"}},
                    // w/ or w/o a comma at the end

                    foreach (string followJson in followsJsonList)
                    {
                        string userJson = followJson.GetBetween("\"user\":{", ",\"type\":\"user\"}");
                        // userJson = "_id":79242000,"name":"volt_100","created_at":"2015-01-08T17:08:01Z","updated_at":"2015-06-20T16:41:10Z","_links":{"self":"https://api.twitch.tv/kraken/users/volt_100"},"display_name":"volt_100","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/volt_100-profile_image-653aa9b8c5343c09-300x300.jpeg","bio":null

                        SqlTwitchUser user = ParseUser(userJson);
                        if (user != null)
                        {
                            SqlTwitchFollower follower = new SqlTwitchFollower(user, channel);
                            followers.AddLast(follower);
                        }
                    }
                }

                // TODO for later, followerCount = uint.Parse(SoberString.TextBetween(json, "],\"_total\":", ",", false, true));
            }

            return followers.ToArray<SqlTwitchFollower>();

        }

        internal static SqlTwitchChannel[] ParseStreams(string json)
        {
            LinkedList<SqlTwitchChannel> liveChannels = new LinkedList<SqlTwitchChannel>();

            if (json != null)
            {
                // {"streams":[{"_id":15979305936,"game":"Hearthstone: Heroes of Warcraft","viewers":32125,"created_at":"2015-08-24T18:43:05Z","video_height":1080,"average_fps":59.9971194327,"is_playlist":false,"_links":{"self":"http://api.twitch.tv/kraken/streams/trumpsc"},"preview":{"small":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-80x45.jpg","medium":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-320x180.jpg","large":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-640x360.jpg","template":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-{width}x{height}.jpg"},"channel":{"_links":{"self":"http://api.twitch.tv/kraken/channels/trumpsc","follows":"http://api.twitch.tv/kraken/channels/trumpsc/follows","commercial":"http://api.twitch.tv/kraken/channels/trumpsc/commercial","stream_key":"http://api.twitch.tv/kraken/channels/trumpsc/stream_key","chat":"http://api.twitch.tv/kraken/chat/trumpsc","features":"http://api.twitch.tv/kraken/channels/trumpsc/features","subscriptions":"http://api.twitch.tv/kraken/channels/trumpsc/subscriptions","editors":"http://api.twitch.tv/kraken/channels/trumpsc/editors","videos":"http://api.twitch.tv/kraken/channels/trumpsc/videos","teams":"http://api.twitch.tv/kraken/channels/trumpsc/teams"},"background":null,"banner":null,"broadcaster_language":"en","display_name":"TrumpSC","game":"Hearthstone: Heroes of Warcraft","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-profile_image-0eca634ef027d36b-300x300.png","mature":false,"status":"TSM Trump New Shaman Ladders","partner":true,"url":"http://www.twitch.tv/trumpsc","video_banner":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-channel_offline_image-7a8982acc68970b3-640x360.jpeg","_id":14836307,"name":"trumpsc","created_at":"2010-08-21T02:09:05Z","updated_at":"2015-08-24T20:17:30Z","delay":0,"followers":586748,"profile_banner":null,"profile_banner_background_color":null,"views":74490399,"language":"en"}},{"_id":15980940288,"game":"Trials Fusion","viewers":8,"created_at":"2015-08-24T20:16:57Z","video_height":0,"average_fps":28.9494117647,"is_playlist":false,"_links":{"self":"https://api.twitch.tv/kraken/streams/coolkid661"},"preview":{"small":"http://static-cdn.jtvnw.net/previews-ttv/live_user_coolkid661-80x45.jpg","medium":"http://static-cdn.jtvnw.net/previews-ttv/live_user_coolkid661-320x180.jpg","large":"http://static-cdn.jtvnw.net/previews-ttv/live_user_coolkid661-640x360.jpg","template":"http://static-cdn.jtvnw.net/previews-ttv/live_user_coolkid661-{width}x{height}.jpg"},"channel":{"_links":{"self":"https://api.twitch.tv/kraken/channels/coolkid661","follows":"https://api.twitch.tv/kraken/channels/coolkid661/follows","commercial":"https://api.twitch.tv/kraken/channels/coolkid661/commercial","stream_key":"https://api.twitch.tv/kraken/channels/coolkid661/stream_key","chat":"https://api.twitch.tv/kraken/chat/coolkid661","features":"https://api.twitch.tv/kraken/channels/coolkid661/features","subscriptions":"https://api.twitch.tv/kraken/channels/coolkid661/subscriptions","editors":"https://api.twitch.tv/kraken/channels/coolkid661/editors","videos":"https://api.twitch.tv/kraken/channels/coolkid661/videos","teams":"https://api.twitch.tv/kraken/channels/coolkid661/teams"},"background":null,"banner":null,"broadcaster_language":"en","display_name":"Coolkid661","game":"Trials Fusion","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/coolkid661-profile_image-c87d780344e2e2bf-300x300.jpeg","mature":true,"status":"Trials pot roast","partner":false,"url":"http://www.twitch.tv/coolkid661","video_banner":"http://static-cdn.jtvnw.net/jtv_user_pictures/coolkid661-channel_offline_image-ebc8ecb7816b3e01-640x360.png","_id":62621803,"name":"coolkid661","created_at":"2014-05-14T21:14:07Z","updated_at":"2015-08-24T20:18:04Z","delay":0,"followers":904,"profile_banner":"http://static-cdn.jtvnw.net/jtv_user_pictures/coolkid661-profile_banner-cdcf6f682eb11f44-480.png","profile_banner_background_color":"null","views":6932,"language":"en"}}],"_total":2,"_links":{"self":"https://api.twitch.tv/kraken/streams?channel=hardlysober%2Csoberbot%2Carbedii%2Cbacon_donut%2Csevadus%2Ctrumpsc%2Ccoolkid661&limit=25&offset=0","next":"https://api.twitch.tv/kraken/streams?channel=hardlysober%2Csoberbot%2Carbedii%2Cbacon_donut%2Csevadus%2Ctrumpsc%2Ccoolkid661&limit=25&offset=25","featured":"https://api.twitch.tv/kraken/streams/featured","summary":"https://api.twitch.tv/kraken/streams/summary","followed":"https://api.twitch.tv/kraken/streams/followed"}}

                json = json.GetBetween("[", "]", true, false);
                // {"_id":15979305936,"game":"Hearthstone: Heroes of Warcraft","viewers":32125,"created_at":"2015-08-24T18:43:05Z","video_height":1080,"average_fps":59.9971194327,"is_playlist":false,"_links":{"self":"http://api.twitch.tv/kraken/streams/trumpsc"},"preview":{"small":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-80x45.jpg","medium":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-320x180.jpg","large":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-640x360.jpg","template":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-{width}x{height}.jpg"},"channel":{"_links":{"self":"http://api.twitch.tv/kraken/channels/trumpsc","follows":"http://api.twitch.tv/kraken/channels/trumpsc/follows","commercial":"http://api.twitch.tv/kraken/channels/trumpsc/commercial","stream_key":"http://api.twitch.tv/kraken/channels/trumpsc/stream_key","chat":"http://api.twitch.tv/kraken/chat/trumpsc","features":"http://api.twitch.tv/kraken/channels/trumpsc/features","subscriptions":"http://api.twitch.tv/kraken/channels/trumpsc/subscriptions","editors":"http://api.twitch.tv/kraken/channels/trumpsc/editors","videos":"http://api.twitch.tv/kraken/channels/trumpsc/videos","teams":"http://api.twitch.tv/kraken/channels/trumpsc/teams"},"background":null,"banner":null,"broadcaster_language":"en","display_name":"TrumpSC","game":"Hearthstone: Heroes of Warcraft","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-profile_image-0eca634ef027d36b-300x300.png","mature":false,"status":"TSM Trump New Shaman Ladders","partner":true,"url":"http://www.twitch.tv/trumpsc","video_banner":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-channel_offline_image-7a8982acc68970b3-640x360.jpeg","_id":14836307,"name":"trumpsc","created_at":"2010-08-21T02:09:05Z","updated_at":"2015-08-24T20:17:30Z","delay":0,"followers":586748,"profile_banner":null,"profile_banner_background_color":null,"views":74490399,"language":"en"}},{"_id":15980940288,"game":"Trials Fusion","viewers":8,"created_at":"2015-08-24T20:16:57Z","video_height":0,"average_fps":28.9494117647,"is_playlist":false,"_links":{"self":"https://api.twitch.tv/kraken/streams/coolkid661"},"preview":{"small":"http://static-cdn.jtvnw.net/previews-ttv/live_user_coolkid661-80x45.jpg","medium":"http://static-cdn.jtvnw.net/previews-ttv/live_user_coolkid661-320x180.jpg","large":"http://static-cdn.jtvnw.net/previews-ttv/live_user_coolkid661-640x360.jpg","template":"http://static-cdn.jtvnw.net/previews-ttv/live_user_coolkid661-{width}x{height}.jpg"},"channel":{"_links":{"self":"https://api.twitch.tv/kraken/channels/coolkid661","follows":"https://api.twitch.tv/kraken/channels/coolkid661/follows","commercial":"https://api.twitch.tv/kraken/channels/coolkid661/commercial","stream_key":"https://api.twitch.tv/kraken/channels/coolkid661/stream_key","chat":"https://api.twitch.tv/kraken/chat/coolkid661","features":"https://api.twitch.tv/kraken/channels/coolkid661/features","subscriptions":"https://api.twitch.tv/kraken/channels/coolkid661/subscriptions","editors":"https://api.twitch.tv/kraken/channels/coolkid661/editors","videos":"https://api.twitch.tv/kraken/channels/coolkid661/videos","teams":"https://api.twitch.tv/kraken/channels/coolkid661/teams"},"background":null,"banner":null,"broadcaster_language":"en","display_name":"Coolkid661","game":"Trials Fusion","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/coolkid661-profile_image-c87d780344e2e2bf-300x300.jpeg","mature":true,"status":"Trials pot roast","partner":false,"url":"http://www.twitch.tv/coolkid661","video_banner":"http://static-cdn.jtvnw.net/jtv_user_pictures/coolkid661-channel_offline_image-ebc8ecb7816b3e01-640x360.png","_id":62621803,"name":"coolkid661","created_at":"2014-05-14T21:14:07Z","updated_at":"2015-08-24T20:18:04Z","delay":0,"followers":904,"profile_banner":"http://static-cdn.jtvnw.net/jtv_user_pictures/coolkid661-profile_banner-cdcf6f682eb11f44-480.png","profile_banner_background_color":"null","views":6932,"language":"en"}}

                if (json != null)
                {
                    string[] channelsJsonList = json.Tokenize("{\"_id\":", true);

                    // channelsList[i] = {"_id":15979305936,"game":"Hearthstone: Heroes of Warcraft","viewers":32125,"created_at":"2015-08-24T18:43:05Z","video_height":1080,"average_fps":59.9971194327,"is_playlist":false,"_links":{"self":"http://api.twitch.tv/kraken/streams/trumpsc"},"preview":{"small":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-80x45.jpg","medium":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-320x180.jpg","large":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-640x360.jpg","template":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-{width}x{height}.jpg"},"channel":{"_links":{"self":"http://api.twitch.tv/kraken/channels/trumpsc","follows":"http://api.twitch.tv/kraken/channels/trumpsc/follows","commercial":"http://api.twitch.tv/kraken/channels/trumpsc/commercial","stream_key":"http://api.twitch.tv/kraken/channels/trumpsc/stream_key","chat":"http://api.twitch.tv/kraken/chat/trumpsc","features":"http://api.twitch.tv/kraken/channels/trumpsc/features","subscriptions":"http://api.twitch.tv/kraken/channels/trumpsc/subscriptions","editors":"http://api.twitch.tv/kraken/channels/trumpsc/editors","videos":"http://api.twitch.tv/kraken/channels/trumpsc/videos","teams":"http://api.twitch.tv/kraken/channels/trumpsc/teams"},"background":null,"banner":null,"broadcaster_language":"en","display_name":"TrumpSC","game":"Hearthstone: Heroes of Warcraft","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-profile_image-0eca634ef027d36b-300x300.png","mature":false,"status":"TSM Trump New Shaman Ladders","partner":true,"url":"http://www.twitch.tv/trumpsc","video_banner":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-channel_offline_image-7a8982acc68970b3-640x360.jpeg","_id":14836307,"name":"trumpsc","created_at":"2010-08-21T02:09:05Z","updated_at":"2015-08-24T20:17:30Z","delay":0,"followers":586748,"profile_banner":null,"profile_banner_background_color":null,"views":74490399,"language":"en"}},
                    // w/ or w/o a comma at the end

                    foreach (string channelJson in channelsJsonList)
                    {
                        SqlTwitchChannel channel = ParseChannel(channelJson);
                        if (channel != null)
                        {
                            liveChannels.AddLast(channel);
                        }
                    }
                }
            }

            return liveChannels.ToArray<SqlTwitchChannel>();
        }

        static SqlTwitchChannel ParseChannel(string json)
        {
            // json = {"_id":15979305936,"game":"Hearthstone: Heroes of Warcraft","viewers":32125,"created_at":"2015-08-24T18:43:05Z","video_height":1080,"average_fps":59.9971194327,"is_playlist":false,"_links":{"self":"http://api.twitch.tv/kraken/streams/trumpsc"},"preview":{"small":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-80x45.jpg","medium":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-320x180.jpg","large":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-640x360.jpg","template":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-{width}x{height}.jpg"},"channel":{"_links":{"self":"http://api.twitch.tv/kraken/channels/trumpsc","follows":"http://api.twitch.tv/kraken/channels/trumpsc/follows","commercial":"http://api.twitch.tv/kraken/channels/trumpsc/commercial","stream_key":"http://api.twitch.tv/kraken/channels/trumpsc/stream_key","chat":"http://api.twitch.tv/kraken/chat/trumpsc","features":"http://api.twitch.tv/kraken/channels/trumpsc/features","subscriptions":"http://api.twitch.tv/kraken/channels/trumpsc/subscriptions","editors":"http://api.twitch.tv/kraken/channels/trumpsc/editors","videos":"http://api.twitch.tv/kraken/channels/trumpsc/videos","teams":"http://api.twitch.tv/kraken/channels/trumpsc/teams"},"background":null,"banner":null,"broadcaster_language":"en","display_name":"TrumpSC","game":"Hearthstone: Heroes of Warcraft","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-profile_image-0eca634ef027d36b-300x300.png","mature":false,"status":"TSM Trump New Shaman Ladders","partner":true,"url":"http://www.twitch.tv/trumpsc","video_banner":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-channel_offline_image-7a8982acc68970b3-640x360.jpeg","_id":14836307,"name":"trumpsc","created_at":"2010-08-21T02:09:05Z","updated_at":"2015-08-24T20:17:30Z","delay":0,"followers":586748,"profile_banner":null,"profile_banner_background_color":null,"views":74490399,"language":"en"}},
            // w/ or w/o a comma at the end

            json = json.GetBetween("{", "}", true, false);
            // "_id":15979305936,"game":"Hearthstone: Heroes of Warcraft","viewers":32125,"created_at":"2015-08-24T18:43:05Z","video_height":1080,"average_fps":59.9971194327,"is_playlist":false,"_links":{"self":"http://api.twitch.tv/kraken/streams/trumpsc"},"preview":{"small":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-80x45.jpg","medium":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-320x180.jpg","large":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-640x360.jpg","template":"http://static-cdn.jtvnw.net/previews-ttv/live_user_trumpsc-{width}x{height}.jpg"},"channel":{"_links":{"self":"http://api.twitch.tv/kraken/channels/trumpsc","follows":"http://api.twitch.tv/kraken/channels/trumpsc/follows","commercial":"http://api.twitch.tv/kraken/channels/trumpsc/commercial","stream_key":"http://api.twitch.tv/kraken/channels/trumpsc/stream_key","chat":"http://api.twitch.tv/kraken/chat/trumpsc","features":"http://api.twitch.tv/kraken/channels/trumpsc/features","subscriptions":"http://api.twitch.tv/kraken/channels/trumpsc/subscriptions","editors":"http://api.twitch.tv/kraken/channels/trumpsc/editors","videos":"http://api.twitch.tv/kraken/channels/trumpsc/videos","teams":"http://api.twitch.tv/kraken/channels/trumpsc/teams"},"background":null,"banner":null,"broadcaster_language":"en","display_name":"TrumpSC","game":"Hearthstone: Heroes of Warcraft","logo":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-profile_image-0eca634ef027d36b-300x300.png","mature":false,"status":"TSM Trump New Shaman Ladders","partner":true,"url":"http://www.twitch.tv/trumpsc","video_banner":"http://static-cdn.jtvnw.net/jtv_user_pictures/trumpsc-channel_offline_image-7a8982acc68970b3-640x360.jpeg","_id":14836307,"name":"trumpsc","created_at":"2010-08-21T02:09:05Z","updated_at":"2015-08-24T20:17:30Z","delay":0,"followers":586748,"profile_banner":null,"profile_banner_background_color":null,"views":74490399,"language":"en"}

            uint userId = uint.Parse(json.GetBetween(",\"_id\":", ","));
            string previewImageUrl = json.GetBetween("\"large\":\"", "\"");
            string game = json.GetBetween("\"game\":\"", "\"");
			uint liveViewers = 0;
			uint.TryParse(json.GetBetween("\"viewers\":", ","), out liveViewers);
			uint totalViews = 0;
			uint.TryParse(json.GetBetween("\"views\":", ","), out totalViews);
			uint followers = 0;
			uint.TryParse(json.GetBetween("\"followers\":", ","), out followers);

            SqlTwitchChannel channel = new SqlTwitchChannel(new SqlTwitchUser(userId), true, previewImageUrl, game, liveViewers, totalViews, followers);
            channel.Save(false);

            return channel;
        }
    }
}