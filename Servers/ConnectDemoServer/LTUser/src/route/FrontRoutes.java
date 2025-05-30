package route;

import com.jfinal.config.Routes;

import controller.UserCtrl;

public class FrontRoutes extends Routes {

	@Override
	public void config() {
		//add("/hello", HelloJFinal.class);
		add("/user", UserCtrl.class);
	}

}
