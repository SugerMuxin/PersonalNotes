package com.joymeng.services.core.buffer;

import java.io.IOException;
import java.io.OutputStream;

public class BufferKit {
	public static JoyBuffer writeRedirct() {
		JoyBuffer buff = JoyBuffer.allocate(51200);
		buff.putInt(0); // 返回0 则为成功
		buff.put((byte) 0xFF);// paramJoyBuffer.put(this.protocolID);
		return buff;
	}

	public static JoyBuffer writeLogic() {
		JoyBuffer buff = JoyBuffer.allocate(51200);
		buff.putInt(0);
		buff.put((byte) 1);// paramJoyBuffer.put(this.protocolID);
		buff.putInt(1);// paramJoyBuffer.putInt(this.destInstanceID);
		buff.putInt(1);// paramJoyBuffer.putInt(this.srcInstanceID);
		buff.putLong(0);// paramJoyBuffer.putLong(this.seqNum);
		buff.putInt(1);// paramJoyBuffer.putInt(this.flag);
		buff.putInt(1111);// messageId
		buff.putLong(0);
		buff.putInt(0);
		buff.putInt(0);
		buff.put((byte) 0);
		return buff;
	}

	public static JoyBuffer writeServerLogin() {
		JoyBuffer buff = JoyBuffer.allocate(51200);
		buff.putInt(0);
		buff.put((byte) 1);// paramJoyBuffer.put(this.protocolID);
		buff.putInt(0xFFFE);// 游戏服务器登陆连接服务器的实例号 模拟
		buff.putInt(1);// paramJoyBuffer.putInt(this.srcInstanceID);
		buff.putLong(0);// paramJoyBuffer.putLong(this.seqNum);
		buff.putInt(1);// paramJoyBuffer.putInt(this.flag);
		buff.putInt(1111);// messageId
		buff.putLong(0);
		buff.putInt(0);
		buff.putInt(0);
		return buff;
	}

	public static JoyBuffer responseClientLogin(byte flag) {
		JoyBuffer buff = JoyBuffer.allocate(51200);
		buff.putInt(0);
		buff.put((byte) 1);// paramJoyBuffer.put(this.protocolID);
		buff.putInt(1);// paramJoyBuffer.putInt(this.destInstanceID);
		buff.putInt(1);// paramJoyBuffer.putInt(this.srcInstanceID);
		buff.putLong(0);// paramJoyBuffer.putLong(this.seqNum);
		buff.putInt(1);// paramJoyBuffer.putInt(this.flag);
		buff.putInt(0x3);// messageId
		buff.putLong(0);
		buff.putInt(0);
		buff.putInt(0);
		buff.put(flag);
		return buff;
	}

	public static void endBuff(OutputStream output, JoyBuffer buff) {
		buff.putInt(0, buff.position() - 4);
		try {
			output.write(buff.arrayToPosition());
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	public static void SkipHeader(JoyBuffer buff) {
		buff.get();
		buff.getInt();
		buff.getInt();
		buff.getLong();
		buff.getInt();
		buff.getInt();
		buff.getLong();
		buff.getInt();
		buff.getInt();
	}
}
