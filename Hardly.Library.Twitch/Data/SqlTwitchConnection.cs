﻿using System;

namespace Hardly {
	public class SqlTwitchConnection : SqlRow {
		public readonly SqlTwitchBot bot;
		public readonly SqlTwitchChannel channel;

		public SqlTwitchConnection(SqlTwitchBot bot, SqlTwitchChannel channel, bool autoConnectToChat = false)
			 : base(new object[] { bot.user.id, channel.user.id, autoConnectToChat }) {
			this.bot = bot;
			this.channel = channel;
		}

		internal static readonly SqlTable _table = new SqlTable("twitch_connections");
		public override SqlTable table {
			get {
				return _table;
			}
		}

		uint botUserId {
			get {
				return Get<uint>(0);
			}
		}

		uint channelUserId {
			get {
				return Get<uint>(1);
			}
		}

		public bool autoConnectToChat {
			get {
				return Get<bool>(2);
			}
			set {
				Set(2, value);
			}
		}

		public static SqlTwitchConnection[] GetAllAutoConnectingConnections(SqlTwitchBot bot) {
			List<object[]> results = _table.Select(null, null, "BotUserId=?a and AutoConnectToChat=?b", new object[] { bot.user.id, true }, null, 0);
			if(results != null && results.Count > 0) {
				SqlTwitchConnection[] connections = new SqlTwitchConnection[results.Count];
				for(int i = 0; i < results.Count; i++) {
					connections[i] = new SqlTwitchConnection(bot, new SqlTwitchChannel(new SqlTwitchUser((uint)results[i][1])), (ulong)results[i][2] != 0);
				}

				return connections;
			}

			return null;
		}
	}
}
