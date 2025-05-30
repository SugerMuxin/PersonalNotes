package controller;

import java.util.Random;
import java.util.UUID;

import model.User;
import net.sf.json.JSONObject;

import com.jfinal.core.Controller;

public class UserCtrl extends Controller {
	public void index() {
		render("../index.html");
	}

	// 快速注册
	public void quickReg() {
		String username = String.valueOf(Math.abs(new Random().nextLong()));
		String password = String.valueOf(Math.abs(new Random().nextLong()));
		renderText(reg(username, password));
	}

	// 注册
	public void reg() {
		String uname = getPara("uname");
		String password = getPara("password");
		renderText(reg(uname, password));
	}

	// 注册
	private String reg(String uname, String pwd) {
		User newu = new User();
		newu.set("username", uname);
		newu.set("password", pwd);
		JSONObject json = new JSONObject();
		if (newu.save()) {
			json.put("status", 1);
			json.put("msg", "注册成功！");
			JSONObject content = new JSONObject();
			content.put("uname", uname);
			content.put("password", pwd);
			json.put("content", content);
		} else {
			json.put("status", 0);
			json.put("msg", "quickReg error!");
		}
		return json.toString();
	}

	// 登陆
	public void login() {
		String uname = getPara("uname");
		String password = getPara("password");
		User u = User.dao.findFirst("select * from users where username='" + uname + "' and password='" + password + "'");
		JSONObject json = new JSONObject();
		if (u != null) {
			u.set("token", UUID.randomUUID().toString());
			u.update();
			json.put("status", 1);
			json.put("msg", "登陆成功！");
			JSONObject content = new JSONObject();
			content.put("uid", u.get("joyid"));
			content.put("token", u.get("token"));
			json.put("content", content);
			System.out.println("账户：" + uname + "登陆啦~\tjoyid：" + u.get("joyid") + "\ttoken:" + u.get("token"));
		} else {
			json.put("status", 0);
			json.put("msg", "账号或密码错误!");
		}
		renderText(json.toString());
	}

}
