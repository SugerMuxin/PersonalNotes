package com.ltgame.esb;

import java.io.IOException;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Random;

import com.joymeng.services.core.buffer.BufferKit;
import com.joymeng.services.core.buffer.JoyBuffer;

/**
 * 
 * 
 * @author Simon.H
 *
 */
public class RedrictServer {

	private ServerSocket ss;
	private Socket socket;
	private OutputStream out;

	public RedrictServer() {
		try {
			ss = new ServerSocket(60000);
			System.out.println("这是一个模拟器的重定向服务器~ 端口60000");
			System.out.println("RedrictServer is waiting client...");
			while (true) {
				socket = ss.accept();
				System.out.println("New Connect!");
				out = socket.getOutputStream();
				JoyBuffer buff = BufferKit.writeRedirct();
				// 这里是有判断的。
				if (new Random().nextBoolean()) {
					// 重定向端口
					buff.putInt(40000);
					// 重定向主机
					buff.putPrefixedString("10.80.1.35");
				} else {
					// 重定向端口
					buff.putInt(40000);
					// 重定向主机
					buff.putPrefixedString("10.80.1.35");
				}
				BufferKit.endBuff(out, buff);
				out.flush();
				out.close();
				socket.close();
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	public static void main(String[] args) {
		new RedrictServer();
	}
}