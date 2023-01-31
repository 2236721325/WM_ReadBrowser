# WM_ReadBrowser
Maui 看小说专用浏览器，去除图片 视频 广告
## Maui WebView 定期执行js脚本

能去除大部分的 

我尝试过拦截 但是没有什么效果

不过这样也不错



目前的 js脚本

```js
var imgs= document.getElementsByTagName("img");
for (let index = imgs.length-1; index >= 0; index--) {
    imgs[index].remove();
}
var videos=document.getElementsByTagName("video");
for (let index = videos.length-1; index >= 0; index--) {
    videos[index].remove();
}
var iframes=document.getElementsByTagName("iframe");
for (let index = iframes.length-1; index >= 0; index--) {
    iframes[index].remove();
}
var divs=document.getElementsByTagName("div");
for (let index = divs.length-1; index >= 0; index--) {
    if(divs[index].style.background.includes("url")){
        divs[index].remove();
}
}
```
