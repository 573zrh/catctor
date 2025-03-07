import Vue from 'vue'
import VueRouter from 'vue-router'
import Router from 'vue-router'
Vue.use(Router)

import Home from '@/components/Home.vue'
import UploadFile from '@/components/UploadFile.vue'
import LayerManage from '@/components/LayerManage.vue'
import ItemManage from '@/components/ItemManage.vue'
const router = new VueRouter({
	routes: [{
		path: "/",
		component: Home
	}, {
		path: "/UploadFile",
		component: UploadFile
	},
	{
		path: "/LayerManage",
		component: LayerManage
	},
	{
		path: "/ItemManage",
		component: ItemManage
	}]
})

export default router;
