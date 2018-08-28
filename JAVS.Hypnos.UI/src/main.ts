import '@babel/polyfill'
import './plugins/vuetify'
import Vue from 'vue';
import App from './App.vue';
import router from './router';
import store from './store';
import SignalRConnectionPlugin, { SignalRConnectionOptions} from "./plugins/signalr-plugin";
import { HubConnectionBuilder, LogLevel } from "@aspnet/signalr";
import { MessagePackHubProtocol } from "@aspnet/signalr-protocol-msgpack";

Vue.config.productionTip = false;


Vue.use<SignalRConnectionOptions>(SignalRConnectionPlugin, {
    urls:["/app", "/faceDetection", "/testHub"],
    builderFactory(): HubConnectionBuilder {
        return new HubConnectionBuilder()
            .configureLogging(LogLevel.Information)
            .withHubProtocol(new MessagePackHubProtocol());
    },
    baseURL: "http://localhost:5005"
});
//192.168.1.68

new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount('#app');
