package conf;

import model.User;
import route.AdminRoutes;
import route.FrontRoutes;

import com.jfinal.config.Constants;
import com.jfinal.config.Handlers;
import com.jfinal.config.Interceptors;
import com.jfinal.config.JFinalConfig;
import com.jfinal.config.Plugins;
import com.jfinal.config.Routes;
import com.jfinal.kit.PropKit;
import com.jfinal.plugin.activerecord.ActiveRecordPlugin;
import com.jfinal.plugin.c3p0.C3p0Plugin;
import com.jfinal.plugin.ehcache.EhCachePlugin;
import com.jfinal.render.ViewType;

public class WebConfig extends JFinalConfig {

	@Override
	public void configConstant(Constants me) {
		me.setViewType(ViewType.FREE_MARKER);
		PropKit.use("config.properties");
		me.setEncoding(PropKit.get("encoding"));
		me.setDevMode(PropKit.getBoolean("devmode"));
	}

	@Override
	public void configRoute(Routes me) {
		me.add(new FrontRoutes()); // 前端
		me.add(new AdminRoutes()); // 后台
	}

	@Override
	public void configPlugin(Plugins me) {
		me.add(new EhCachePlugin());
		C3p0Plugin cp = new C3p0Plugin(PropKit.get("jdbcurl"), PropKit.get("user"), PropKit.get("password"));
		me.add(cp);
		ActiveRecordPlugin arp = new ActiveRecordPlugin(cp);
		me.add(arp);
		arp.addMapping("users", "joyid", User.class);
	}

	@Override
	public void configInterceptor(Interceptors me) {

	}

	@Override
	public void configHandler(Handlers me) {
	}

	@Override
	public void beforeJFinalStop() {
		super.beforeJFinalStop();
	}

	@Override
	public void afterJFinalStart() {
		super.afterJFinalStart();
		System.out.println("这是一个模拟器的账户登陆http服务器~");
	}
}
