(function(t){function e(e){for(var i,o,c=e[0],l=e[1],s=e[2],u=0,d=[];u<c.length;u++)o=c[u],r[o]&&d.push(r[o][0]),r[o]=0;for(i in l)Object.prototype.hasOwnProperty.call(l,i)&&(t[i]=l[i]);f&&f(e);while(d.length)d.shift()();return a.push.apply(a,s||[]),n()}function n(){for(var t,e=0;e<a.length;e++){for(var n=a[e],i=!0,o=1;o<n.length;o++){var l=n[o];0!==r[l]&&(i=!1)}i&&(a.splice(e--,1),t=c(c.s=n[0]))}return t}var i={},r={app:0},a=[];function o(t){return c.p+"js/"+({about:"about"}[t]||t)+"."+{about:"f2dc9309"}[t]+".js"}function c(e){if(i[e])return i[e].exports;var n=i[e]={i:e,l:!1,exports:{}};return t[e].call(n.exports,n,n.exports,c),n.l=!0,n.exports}c.e=function(t){var e=[],n=r[t];if(0!==n)if(n)e.push(n[2]);else{var i=new Promise(function(e,i){n=r[t]=[e,i]});e.push(n[2]=i);var a,l=document.getElementsByTagName("head")[0],s=document.createElement("script");s.charset="utf-8",s.timeout=120,c.nc&&s.setAttribute("nonce",c.nc),s.src=o(t),a=function(e){s.onerror=s.onload=null,clearTimeout(u);var n=r[t];if(0!==n){if(n){var i=e&&("load"===e.type?"missing":e.type),a=e&&e.target&&e.target.src,o=new Error("Loading chunk "+t+" failed.\n("+i+": "+a+")");o.type=i,o.request=a,n[1](o)}r[t]=void 0}};var u=setTimeout(function(){a({type:"timeout",target:s})},12e4);s.onerror=s.onload=a,l.appendChild(s)}return Promise.all(e)},c.m=t,c.c=i,c.d=function(t,e,n){c.o(t,e)||Object.defineProperty(t,e,{enumerable:!0,get:n})},c.r=function(t){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(t,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(t,"__esModule",{value:!0})},c.t=function(t,e){if(1&e&&(t=c(t)),8&e)return t;if(4&e&&"object"===typeof t&&t&&t.__esModule)return t;var n=Object.create(null);if(c.r(n),Object.defineProperty(n,"default",{enumerable:!0,value:t}),2&e&&"string"!=typeof t)for(var i in t)c.d(n,i,function(e){return t[e]}.bind(null,i));return n},c.n=function(t){var e=t&&t.__esModule?function(){return t["default"]}:function(){return t};return c.d(e,"a",e),e},c.o=function(t,e){return Object.prototype.hasOwnProperty.call(t,e)},c.p="/",c.oe=function(t){throw console.error(t),t};var l=window["webpackJsonp"]=window["webpackJsonp"]||[],s=l.push.bind(l);l.push=e,l=l.slice();for(var u=0;u<l.length;u++)e(l[u]);var f=s;a.push([0,"chunk-vendors"]),n()})({0:function(t,e,n){t.exports=n("cd49")},3:function(t,e){},4:function(t,e){},5429:function(t,e,n){"use strict";var i=n("6cda"),r=n.n(i);r.a},"6cda":function(t,e,n){},cd49:function(t,e,n){"use strict";n.r(e);n("744f"),n("6c7b"),n("7514"),n("20d6"),n("1c4c"),n("6762"),n("cadf"),n("e804"),n("55dd"),n("d04f"),n("c8ce"),n("217b"),n("7f7f"),n("f400"),n("7f25"),n("536b"),n("d9ab"),n("f9ab"),n("32d7"),n("25c9"),n("9f3c"),n("042e"),n("c7c6"),n("f4ff"),n("049f"),n("7872"),n("a69f"),n("0b21"),n("6c1a"),n("c7c62"),n("84b4"),n("c5f6"),n("2e37"),n("fca0"),n("7cdf"),n("ee1d"),n("b1b1"),n("87f3"),n("9278"),n("5df2"),n("04ff"),n("f751"),n("4504"),n("fee7"),n("ffc1"),n("0d6d"),n("9986"),n("8e6e"),n("25db"),n("e4f7"),n("b9a1"),n("64d5"),n("9aea"),n("db97"),n("66c8"),n("57f0"),n("165b"),n("456d"),n("cf6a"),n("fd24"),n("8615"),n("551c"),n("097d"),n("df1b"),n("2397"),n("88ca"),n("ba16"),n("d185"),n("ebde"),n("2d34"),n("f6b3"),n("2251"),n("c698"),n("a19f"),n("9253"),n("9275"),n("3b2b"),n("3846"),n("4917"),n("a481"),n("28a5"),n("386d"),n("6b54"),n("4f7f"),n("8a81"),n("ac4d"),n("8449"),n("9c86"),n("fa83"),n("48c0"),n("a032"),n("aef6"),n("d263"),n("6c37"),n("9ec8"),n("5695"),n("2fdb"),n("d0b0"),n("b54a"),n("f576"),n("ed50"),n("788d"),n("14b9"),n("f386"),n("f559"),n("1448"),n("673e"),n("242a"),n("c66f"),n("b05c"),n("34ef"),n("6aa2"),n("15ac"),n("af56"),n("b6e4"),n("9c29"),n("63d9"),n("4dda"),n("10ad"),n("c02b"),n("4795"),n("130f"),n("ac6a"),n("96cf");var i=n("2b0e"),r=n("ce5b"),a=n.n(r);n("bf40");i["default"].use(a.a,{});var o=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("v-app",[n("v-navigation-drawer",{attrs:{persistent:"","mini-variant":t.miniVariant,clipped:t.clipped,"enable-resize-watcher":"",fixed:"",app:""},model:{value:t.drawer,callback:function(e){t.drawer=e},expression:"drawer"}},[n("v-list",t._l(t.items,function(e,i){return n("v-list-tile",{key:i,attrs:{value:"true"}},[n("v-list-tile-action",[n("v-icon",{domProps:{innerHTML:t._s(e.icon)}})],1),n("v-list-tile-content",[n("v-list-tile-title",{domProps:{textContent:t._s(e.title)}})],1)],1)}))],1),n("v-toolbar",{attrs:{app:"","clipped-left":t.clipped}},[n("v-toolbar-side-icon",{on:{click:function(e){e.stopPropagation(),t.drawer=!t.drawer}}}),n("v-btn",{attrs:{icon:""},on:{click:function(e){e.stopPropagation(),t.miniVariant=!t.miniVariant}}},[n("v-icon",{domProps:{innerHTML:t._s(t.miniVariant?"chevron_right":"chevron_left")}})],1),n("v-btn",{attrs:{icon:""},on:{click:function(e){e.stopPropagation(),t.clipped=!t.clipped}}},[n("v-icon",[t._v("web")])],1),n("v-btn",{attrs:{icon:""},on:{click:function(e){e.stopPropagation(),t.fixed=!t.fixed}}},[n("v-icon",[t._v("remove")])],1),n("v-toolbar-title",{domProps:{textContent:t._s(t.title)}}),n("v-spacer"),n("v-btn",{attrs:{icon:""},on:{click:function(e){e.stopPropagation(),t.rightDrawer=!t.rightDrawer}}},[n("v-icon",[t._v("menu")])],1)],1),n("v-content",[n("LiveConfigAlgorithm")],1),n("v-navigation-drawer",{attrs:{temporary:"",right:t.right,fixed:"",app:""},model:{value:t.rightDrawer,callback:function(e){t.rightDrawer=e},expression:"rightDrawer"}},[n("v-list",[n("v-list-tile",{on:{click:function(e){t.right=!t.right}}},[n("v-list-tile-action",[n("v-icon",[t._v("compare_arrows")])],1),n("v-list-tile-title",[t._v("Switch drawer (click me)")])],1)],1)],1),n("v-footer",{attrs:{fixed:t.fixed,app:""}},[n("span",[t._v("© 2017")])])],1)},c=[],l=n("c665"),s=n("dc0a"),u=n("aa9a"),f=n("d328"),d=n("11d9"),p=n("9ab4"),v=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("v-container",{attrs:{fluid:""}},[n("v-slide-y-transition",{attrs:{mode:"out-in"}},[n("v-card",{attrs:{flat:"",color:"transparent"}},[n("v-card-title",[n("h3",[t._v("\n                  Facial Algorithm Configuration (Live)\n              ")])]),n("v-card-text",{staticClass:"pt-0"},[n("v-slider",{attrs:{rules:t.sensitivityRules,label:"Sensitivity",max:"10","thumb-label":"always",ticks:""},model:{value:t.sensitivity,callback:function(e){t.sensitivity=e},expression:"sensitivity"}})],1),n("v-card-text",{staticClass:"pt-0"},[n("v-slider",{attrs:{rules:t.sizeRules,label:"Face Size (distance)","thumb-label":"always",ticks:""},model:{value:t.size,callback:function(e){t.size=e},expression:"size"}})],1)],1)],1)],1)},b=[],h=n("65d9"),g=n.n(h),m=function(t){function e(){var t;return Object(l["a"])(this,e),t=Object(f["a"])(this,Object(d["a"])(e).apply(this,arguments)),t.size=30,t.sensitivity=5,t}return Object(u["a"])(e,[{key:"created",value:function(){var t=this.$signalR["testHub"];t.on("Listen",function(t){console.log(t)}),Promise.all([t.start()]).catch(function(t){return console.error(t)})}},{key:"sensitivityRules",get:function(){return[function(t){return t<=8&&t>=2||"This level of sensitivity produces less than ideal results"}]}},{key:"sizeRules",get:function(){return[function(t){return t>=20||"Smaller face sizes result in a slower running algorithm"}]}}]),Object(s["a"])(e,t),e}(i["default"]);m=p["c"]([g()({props:{message:String}})],m);var y=m,w=y,_=(n("5429"),n("2877")),j=Object(_["a"])(w,v,b,!1,null,"d5deb5f2",null);j.options.__file="LiveConfigAlgorithm.vue";var x=j.exports,O=function(t){function e(){return Object(l["a"])(this,e),Object(f["a"])(this,Object(d["a"])(e).apply(this,arguments))}return Object(u["a"])(e,[{key:"data",value:function(){return{clipped:!1,drawer:!0,fixed:!1,items:[{icon:"bubble_chart",title:"Inspire"}],miniVariant:!1,right:!0,rightDrawer:!1,title:"JAVS Hypnos"}}}]),Object(s["a"])(e,t),e}(i["default"]);O=p["c"]([g()({components:{LiveConfigAlgorithm:x}})],O);var k=O,P=k,S=Object(_["a"])(P,o,c,!1,null,null,null);S.options.__file="App.vue";var C=S.exports,L=n("8c4f"),z=function(){var t=this,e=t.$createElement,i=t._self._c||e;return i("div",{staticClass:"home"},[i("img",{attrs:{alt:"Vue logo",src:n("cf05")}}),i("live-config-algorithm")],1)},T=[],A=n("60a3"),R=function(t){function e(){return Object(l["a"])(this,e),Object(f["a"])(this,Object(d["a"])(e).apply(this,arguments))}return Object(s["a"])(e,t),e}(A["b"]);R=p["c"]([Object(A["a"])({components:{LiveConfigAlgorithm:x}})],R);var E=R,H=E,V=Object(_["a"])(H,z,T,!1,null,null,null);V.options.__file="Home.vue";var D=V.exports;i["default"].use(L["a"]);var M=new L["a"]({routes:[{path:"/",name:"home",component:D},{path:"/about",name:"about",component:function(){return n.e("about").then(n.bind(null,"f820"))}}]}),$=n("2f62");i["default"].use($["a"]);var F=new $["a"].Store({state:{},mutations:{},actions:{}});function J(t){return t.startsWith("/")&&(t=t.slice(1)),t}var I={install:function(t,e){if(!this.install.installed){var n={};e.urls.forEach(function(t){n[J(t)]=e.builderFactory().withUrl(t).build()}),t.mixin({destroyed:function(){console.log("Kill all connections")}}),t.prototype.$signalR=n,this.install.installed=!0,console.log("SignalR plugin installed...")}}},U=I,q=n("1a9a"),B=n("ebe8");i["default"].config.productionTip=!1,i["default"].use(U,{urls:["/app","/second","/testHub"],builderFactory:function(){return(new q["a"]).configureLogging(q["b"].Information).withHubProtocol(new B["a"])},baseURL:"http://192.168.1.68:47333"}),new i["default"]({router:M,store:F,render:function(t){return t(C)}}).$mount("#app")},cf05:function(t,e,n){t.exports=n.p+"img/logo.978a7dfc.png"}});
//# sourceMappingURL=app.f2efa378.js.map