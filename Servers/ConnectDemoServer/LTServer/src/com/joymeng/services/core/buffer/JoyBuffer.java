package com.joymeng.services.core.buffer;

import java.io.UnsupportedEncodingException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;

public class JoyBuffer {

	public static final byte STRING_TYPE_BYTE = 1;
	public static final byte STRING_TYPE_SHORT = 2;
	public static String DEFAULT_CHARSET = "UTF_8";
	public static final byte DEFAULT_STRING_TYPE = 1;
	private ByteBuffer buffer;
	public static final int[] STRING_LEN_MAX = { 0, 255, 65535 };

	public static JoyBuffer allocate(int paramInt) {
		JoyBuffer localJoyBuffer = new JoyBuffer();
		localJoyBuffer.buffer = ByteBuffer.allocate(paramInt);
		return localJoyBuffer;
	}

	public static JoyBuffer wrap(byte[] paramArrayOfByte) {
		return wrap(paramArrayOfByte, true);
	}

	public static JoyBuffer wrap(byte[] paramArrayOfByte, boolean paramBoolean) {
		JoyBuffer localJoyBuffer = new JoyBuffer();
		localJoyBuffer.buffer = ByteBuffer.wrap(paramArrayOfByte);
		return localJoyBuffer;
	}

	public static JoyBuffer wrap(byte[] paramArrayOfByte, int paramInt1, int paramInt2) {
		return wrap(paramArrayOfByte, paramInt1, paramInt2, true);
	}

	public static JoyBuffer wrap(byte[] paramArrayOfByte, int paramInt1, int paramInt2, boolean paramBoolean) {
		JoyBuffer localJoyBuffer = new JoyBuffer();
		localJoyBuffer.buffer = ByteBuffer.wrap(paramArrayOfByte, paramInt1, paramInt2);
		return localJoyBuffer;
	}

	public ByteBuffer buf() {
		return this.buffer;
	}

	public int capacity() {
		return this.buffer.capacity();
	}

	public int position() {
		return this.buffer.position();
	}

	public JoyBuffer position(int paramInt) {
		this.buffer.position(paramInt);
		return this;
	}

	public int limit() {
		return this.buffer.limit();
	}

	public JoyBuffer limit(int paramInt) {
		this.buffer.limit(paramInt);
		return this;
	}

	public JoyBuffer mark() {
		this.buffer.mark();
		return this;
	}

	public JoyBuffer reset() {
		this.buffer.reset();
		return this;
	}

	public JoyBuffer clear() {
		this.buffer.clear();
		return this;
	}

	public JoyBuffer flip() {
		this.buffer.flip();
		return this;
	}

	public JoyBuffer rewind() {
		this.buffer.rewind();
		return this;
	}

	public int remaining() {
		return this.buffer.remaining();
	}

	public boolean hasRemaining() {
		return this.buffer.hasRemaining();
	}

	public JoyBuffer duplicate() {
		JoyBuffer localJoyBuffer = new JoyBuffer();
		localJoyBuffer.buffer = this.buffer.duplicate();
		return localJoyBuffer;
	}

	public JoyBuffer slice() {
		JoyBuffer localJoyBuffer = new JoyBuffer();
		localJoyBuffer.buffer = this.buffer.slice();
		return localJoyBuffer;
	}

	public JoyBuffer slice(int paramInt) {
		JoyBuffer localJoyBuffer = new JoyBuffer();
		localJoyBuffer.buffer = this.buffer.slice();
		localJoyBuffer.limit(paramInt);
		return localJoyBuffer;
	}

	public JoyBuffer sliceNew() {
		return sliceNew(limit() - position());
	}

	public JoyBuffer sliceNew(int paramInt) {
		return wrap(array(), position(), paramInt);
	}

	public byte[] array() {
		return this.buffer.array();
	}

	public byte[] arrayToPosition() {
		int i = position();
		rewind();
		return getByteArray(i);
	}

	public int arrayOffset() {
		return this.buffer.arrayOffset();
	}

	public byte get() {
		return this.buffer.get();
	}

	public JoyBuffer put(byte paramByte) {
		this.buffer.put(paramByte);
		return this;
	}

	public byte get(int paramInt) {
		return this.buffer.get(paramInt);
	}

	public JoyBuffer put(int paramInt, byte paramByte) {
		this.buffer.put(paramInt, paramByte);
		return this;
	}

	public JoyBuffer get(byte[] paramArrayOfByte, int paramInt1, int paramInt2) {
		this.buffer.get(paramArrayOfByte, paramInt1, paramInt2);
		return this;
	}

	public JoyBuffer get(byte[] paramArrayOfByte) {
		this.buffer.get(paramArrayOfByte);
		return this;
	}

	public byte[] getByteArray(int paramInt) {
		byte[] arrayOfByte = new byte[paramInt];
		get(arrayOfByte);
		return arrayOfByte;
	}

	public JoyBuffer put(ByteBuffer paramByteBuffer) {
		this.buffer.put(paramByteBuffer);
		return this;
	}

	public JoyBuffer put(byte[] paramArrayOfByte, int paramInt1, int paramInt2) {
		this.buffer.put(paramArrayOfByte, paramInt1, paramInt2);
		return this;
	}

	public JoyBuffer put(byte[] paramArrayOfByte) {
		this.buffer.put(paramArrayOfByte);
		return this;
	}

	public JoyBuffer compact() {
		this.buffer.compact();
		return this;
	}

	public ByteOrder order() {
		return this.buffer.order();
	}

	public JoyBuffer order(ByteOrder paramByteOrder) {
		this.buffer.order(paramByteOrder);
		return this;
	}

	public char getChar() {
		return this.buffer.getChar();
	}

	public JoyBuffer putChar(char paramChar) {
		this.buffer.putChar(paramChar);
		return this;
	}

	public char getChar(int paramInt) {
		return this.buffer.getChar(paramInt);
	}

	public JoyBuffer putChar(int paramInt, char paramChar) {
		this.buffer.putChar(paramInt, paramChar);
		return this;
	}

	public short getShort() {
		return this.buffer.getShort();
	}

	public JoyBuffer putShort(short paramShort) {
		this.buffer.putShort(paramShort);
		return this;
	}

	public short getShort(int paramInt) {
		return this.buffer.getShort(paramInt);
	}

	public JoyBuffer putShort(int paramInt, short paramShort) {
		this.buffer.putShort(paramInt, paramShort);
		return this;
	}

	public int getInt() {
		return this.buffer.getInt();
	}

	public JoyBuffer putInt(int paramInt) {
		this.buffer.putInt(paramInt);
		return this;
	}

	public int getInt(int paramInt) {
		return this.buffer.getInt(paramInt);
	}

	public JoyBuffer putInt(int paramInt1, int paramInt2) {
		this.buffer.putInt(paramInt1, paramInt2);
		return this;
	}

	public long getLong() {
		return this.buffer.getLong();
	}

	public JoyBuffer putLong(long paramLong) {
		this.buffer.putLong(paramLong);
		return this;
	}

	public long getLong(int paramInt) {
		return this.buffer.getLong(paramInt);
	}

	public JoyBuffer putLong(int paramInt, long paramLong) {
		this.buffer.putLong(paramInt, paramLong);
		return this;
	}

	public float getFloat() {
		return this.buffer.getFloat();
	}

	public JoyBuffer putFloat(float paramFloat) {
		this.buffer.putFloat(paramFloat);
		return this;
	}

	public float getFloat(int paramInt) {
		return this.buffer.getFloat(paramInt);
	}

	public JoyBuffer putFloat(int paramInt, float paramFloat) {
		this.buffer.putFloat(paramInt, paramFloat);
		return this;
	}

	public double getDouble() {
		return this.buffer.getDouble();
	}

	public JoyBuffer putDouble(double paramDouble) {
		this.buffer.putDouble(paramDouble);
		return this;
	}

	public double getDouble(int paramInt) {
		return this.buffer.getDouble(paramInt);
	}

	public JoyBuffer putDouble(int paramInt, double paramDouble) {
		this.buffer.putDouble(paramInt, paramDouble);
		return this;
	}

	public String getPrefixedString() {
		return getPrefixedString(1);
	}

	public String getPrefixedString(int paramInt) {
		int len = 0;
		if (paramInt == 1) {
			len = get();
		} else {
			len = getShort();
		}
		byte[] bytes = new byte[len];
		get(bytes);
		try {
			return new String(bytes, "UTF-8");
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		}
		return "";
	}

	public JoyBuffer putPrefixedString(String paramString) {
		return putPrefixedString(paramString, DEFAULT_STRING_TYPE);
	}

	public JoyBuffer putPrefixedString(String paramString, byte paramByte) {
		try {
			byte[] bytes = paramString.getBytes("UTF-8");
			if (paramByte == 1) {
				put((byte) bytes.length);
			} else {
				putShort((short) bytes.length);
			}
			return put(bytes);
		} catch (UnsupportedEncodingException e) {
			e.printStackTrace();
		}
		return this;
	}

	public JoyBuffer skip(int paramInt) {
		while (paramInt-- > 0) {
			this.buffer.put((byte) 0);
		}
		return this;
	}

}
