package com.ltgame.game;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;

import com.joymeng.services.core.buffer.BufferKit;
import com.joymeng.services.core.buffer.JoyBuffer;

/**
 * 模拟游戏服务器1 实例号1
 * 
 * @author Simon.H
 *
 */
public class GameServer implements Runnable {
	// 服务器实例号
	private long instanceid;

	public GameServer(long id) {
		this.instanceid = id;
	}

	@Override
	public void run() {
		boolean flag = true;
		// 也得去登陆连接服务器
		try {
			Socket sk = new Socket("10.80.1.87", 40000);
			while (!sk.isConnected()) {
				System.out.println("Wait to connect base server...");
				try {
					Thread.sleep(500);
				} catch (InterruptedException e) {
					e.printStackTrace();
				}
			}
			// 类似客户端登陆连接服务器的处理
			DataInputStream in = new DataInputStream(sk.getInputStream());
			DataOutputStream out = new DataOutputStream(sk.getOutputStream());
			JoyBuffer login = BufferKit.writeServerLogin();
			login.putLong(instanceid);
			BufferKit.endBuff(out, login);
			out.flush();
			while (flag) {
				if (in.available() < 4) {
					Thread.sleep(300);
					continue;
				}
				int datalen = in.readInt();
				System.out.println(datalen);
				byte[] bytes = new byte[datalen];
				in.readFully(bytes);
				JoyBuffer buff = JoyBuffer.wrap(bytes);
				buff.mark();
				buff.get();// (this.protocolID);
				buff.getInt();// (this.destInstanceID);
				buff.getInt();// (this.srcInstanceID);
				buff.getLong();// (this.seqNum);
				buff.getInt();// (this.flag);
				int messageid = buff.getInt();// messageId
				if (messageid != 1111) {
					continue;
				}
				long joyid = buff.getLong(); // joyid
				buff.getInt(); // 保留
				buff.getInt(); // 保留
				int clientModelId = buff.getInt();
				if (clientModelId == 999) {
					System.out.println(buff.getPrefixedString((byte) 2));
				}
				JoyBuffer buffer = JoyBuffer.allocate(51200);
				buffer.putInt(0);
				buffer.put((byte) 1);// paramJoyBuffer.put(this.protocolID);
				buffer.putInt(1);// paramJoyBuffer.putInt(this.destInstanceID);
				buffer.putInt(1);// paramJoyBuffer.putInt(this.srcInstanceID);
				buffer.putLong(0);// paramJoyBuffer.putLong(this.seqNum);
				buffer.putInt(1);// paramJoyBuffer.putInt(this.flag);
				buffer.putInt(1111);// messageId
				buffer.putLong(joyid); // joyid
				buffer.putInt(0);
				buffer.putInt(0);
				buffer.put((byte) 0);
				buffer.putInt(1); // messagecode
				buffer.put((byte) 1); // message data model num
				buffer.putShort((short) 999); // data model code
				buffer.putPrefixedString("Hello Client~ From GameServer instance " + instanceid + "!", (byte) 2);
				BufferKit.endBuff(out, buffer);
				out.flush();
			}
			sk.close();
		} catch (IOException | InterruptedException e) {
			flag = false;
			e.printStackTrace();
		}
	}

	public static void main(String[] args) {
		new Thread(new GameServer(1)).start();
		new Thread(new GameServer(2)).start();
	}
}
