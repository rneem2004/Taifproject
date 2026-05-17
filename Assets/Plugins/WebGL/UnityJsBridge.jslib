mergeInto(LibraryManager.library, {
  // ── Called by C# on Start() to signal readiness ─────────────────────
  UJSB_NotifyReady: function () {
    console.log('[jslib] UJSB_NotifyReady fired — Unity C# is alive');//////////
    window.__unityReady = true;
    // Fire a DOM event so the HTML page can react
    window.dispatchEvent(new CustomEvent('unityReady'));
    // Direct callback support
    if (typeof window.onUnityReady === 'function') {
      window.onUnityReady();
    }
  },
  // ── C# → HTML page ───────────────────────────────────────────────────
  UJSB_SendToJS: function (msgPtr) {
    // UTF8ToString replaces deprecated Pointer_stringify in Unity 2022.x
    var msg = UTF8ToString(msgPtr);
    console.log('[jslib] UJSB_SendToJS:', msg);/////////
    window.dispatchEvent(new CustomEvent('unityToJS', { detail: msg }));
    if (typeof window.onUnityToJS === 'function') {
      window.onUnityToJS(msg);
    }
  },
  // ── C# → Flutter ─────────────────────────────────────────────────────
  UJSB_SendToFlutter: function (msgPtr) {
    var msg = UTF8ToString(msgPtr);
    console.log('[jslib] UJSB_SendToFlutter:', msg);
    // 1. flutter_inappwebview (preferred)
    if (window.flutter_inappwebview) {
      window.flutter_inappwebview.callHandler('UnityChannel', msg);
      return;
    }
    // 2. webview_flutter JavascriptChannel
    if (window.UnityChannel) {
      window.UnityChannel.postMessage(msg);
      return;
    }
    // 3. iframe / postMessage fallback
    window.parent.postMessage({ source: 'unity', payload: msg }, '*');
  }
});