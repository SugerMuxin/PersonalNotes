package com.ltgame.esb;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.concurrent.ArrayBlockingQueue;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.ThreadPoolExecutor;
import java.util.concurrent.TimeUnit;

import com.jfinal.plugin.activerecord.ActiveRecordPlugin;
import com.jfinal.plugin.c3p0.C3p0Plugin;
import com.joymeng.services.core.buffer.BufferKit;
import com.joymeng.services.core.buffer.JoyBuffer;

/**
 * 模拟连接服务器 端口40000
 * 
 * @author Simon.H
 *
 */
public class ConnectServer {
	ThreadPoolExecutor pool;
	private ServerSocket ss;
	private static ConcurrentHashMap<Long, SocketConnect> clientConnects = new ConcurrentHashMap<Long, SocketConnect>();
	private static ConcurrentHashMap<Long, SocketConnect> gameServerConnects = new ConcurrentHashMap<Long, SocketConnect>();

	private class SocketConnect implements Runnable {
		public ArrayBlockingQueue<byte[]> inBufferCache = new ArrayBlockingQueue<byte[]>(999);
		public ArrayBlockingQueue<byte[]> outBufferCache = new ArrayBlockingQueue<byte[]>(999);
		private long connectId; // 唯一id 对于服务器来说就是instanceid 对于客户端玩家来说就是joyid

		private byte connectType = 0; // 0 客户端 1游戏服务器
		private Socket sk;

		public SocketConnect(Socket sk) {
			this.sk = sk;
		}

		public void addInCache(byte[] cache) {
			inBufferCache.add(cache);
		}

		public void addOutCache(byte[] cache) {
			outBufferCache.add(cache);
		}

		/**
		 * 这个Runnable只负责缓存接受的数据
		 */
		@Override
		public void run() {
			boolean flag = true;
			try {
				DataInputStream in = new DataInputStream(sk.getInputStream());
				DataOutputStream out = new DataOutputStream(sk.getOutputStream());
				long startTime = System.currentTimeMillis();
				// 缓存Socket收到的信息
				while (flag) {
					// 读缓存
					if (!inBufferCache.isEmpty()) {
						byte[] cachebytes = inBufferCache.poll();
						while (cachebytes != null) {
							JoyBuffer cache = JoyBuffer.wrap(cachebytes);
							cache.mark();
							byte protocolID = cache.get(); // 协议号
							int instanceid = cache.getInt();
							// 客户端登陆
							if (protocolID == 1 && instanceid == 0xFFFF) {
								cache.reset();
								// 处理账户校验相关问题 这里服务器会校验之前登陆账户服务器生成的joyid和token
								BufferKit.SkipHeader(cache);
								long joyid = cache.getLong();
								String token = cache.getPrefixedString((byte) 2);
								if (checkUser(joyid, token)) {
									// 校验成功 返回0 告诉客户端 你可以和游戏服务器交互了
									JoyBuffer response = BufferKit.responseClientLogin((byte) 0);
									BufferKit.endBuff(out, response);
									out.flush();
									if (clientConnects.containsKey(joyid)) {
										pool.remove(clientConnects.get(joyid));
										clientConnects.remove(joyid);
									}
									connectType = 0;
									connectId = joyid;
									clientConnects.put(joyid, this);
									System.out.println("New Client >>> joyid:" + joyid + "\ttoken:" + token);
								} else {
									// 校验失败 返回非0给客户端 并断开连接
									JoyBuffer response = BufferKit.responseClientLogin((byte) 1);
									BufferKit.endBuff(out, response);
									out.flush();
									sk.close();
									flag = false;
								}
							}
							// 游戏服务器登陆 模拟
							else if (protocolID == 1 && instanceid == 0xFFFE) {
								cache.reset();
								BufferKit.SkipHeader(cache);
								long gameServerInstanceId = cache.getLong();
								if (gameServerConnects.containsKey(gameServerInstanceId)) {
									pool.remove(gameServerConnects.get(gameServerInstanceId));
									gameServerConnects.remove(gameServerInstanceId);
								}
								connectId = gameServerInstanceId;
								connectType = 1;
								gameServerConnects.put(gameServerInstanceId, this);
								JoyBuffer response = BufferKit.responseClientLogin((byte) 5);
								BufferKit.endBuff(out, response);
							} else if (protocolID == 1) {
								cache.getInt(); // paramJoyBuffer.putInt(this.srcInstanceID);
								cache.getLong(); // paramJoyBuffer.putLong(this.seqNum);
								cache.getInt(); // paramJoyBuffer.putInt(this.flag);
								cache.getInt(); // messageId
								if (connectType == 0) { // 客户端
									cache.putLong(cache.position(), connectId);
									cache.reset();
									gameServerConnects.get(new Long(instanceid)).addOutCache(cache.getByteArray(cachebytes.length));
								} else if (connectType == 1) { // 游戏服务器
									long joyid = cache.getLong();
									clientConnects.get(joyid).addOutCache(cachebytes);
								}
							}
							cachebytes = inBufferCache.poll();
						}
					}
					// 写缓存
					if (!outBufferCache.isEmpty()) {
						byte[] cachebytes = outBufferCache.poll();
						while (cachebytes != null) {
							JoyBuffer buffer = JoyBuffer.allocate(51200);
							buffer.putInt(cachebytes.length);
							buffer.put(cachebytes);
							BufferKit.endBuff(out, buffer);
							out.flush();
							cachebytes = outBufferCache.poll();
						}
					}
					if (System.currentTimeMillis() - startTime > 10 * 60 * 1000) {
						flag = false;
					}
					if (in.available() < 4) {
						Thread.sleep(300);
						continue;
					}
					startTime = System.currentTimeMillis();
					int datalen = in.readInt();
					byte[] bytes = new byte[datalen];
					in.readFully(bytes);
					addInCache(bytes);
				}
				sk.close();
			} catch (IOException | InterruptedException e) {
				e.printStackTrace();
				flag = false;
			}

		}
	}

	public ConnectServer() {
		try {
			// 开个Socket连接线程池
			pool = new ThreadPoolExecutor(10, 10, 0L, TimeUnit.MILLISECONDS, new LinkedBlockingQueue<Runnable>());
			ss = new ServerSocket(40000);
			System.out.println("这是一个模拟器的连接服务器~ 端口40000");
			System.out.println("ConnectServer is waiting client...");
			while (true) {
				Socket socket = ss.accept();
				pool.execute(new SocketConnect(socket));
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	private boolean checkUser(long joyid, String token) {
		User u = User.dao.findFirst("select * from users where joyid='" + joyid + "' and token='" + token + "'");
		return u != null;
	}

	public static void main(String[] args) {
		// 连接用户数据库
		C3p0Plugin dp = new C3p0Plugin("jdbc:mysql://localhost/ltuser", "root", "root");
		ActiveRecordPlugin arp = new ActiveRecordPlugin(dp);
		arp.addMapping("users", "joyid", User.class);
		dp.start();
		arp.start();
		new ConnectServer();
	}
}
