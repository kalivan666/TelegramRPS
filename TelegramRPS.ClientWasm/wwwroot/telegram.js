window.TelegramLogin = {
    initData: '',
    initDataUnsafe: {},
};

window.addEventListener("DOMContentLoaded", () => {
    if (window.Telegram && Telegram.WebApp) {
        Telegram.WebApp.ready();
        window.TelegramLogin.initData = Telegram.WebApp.initData;
        window.TelegramLogin.initDataUnsafe = Telegram.WebApp.initDataUnsafe;
        console.log("Telegram initData:", Telegram.WebApp.initData);
        console.log("Telegram WebApp user data:", Telegram.WebApp.initDataUnsafe.user);
    } else {
        console.warn("Not running in Telegram WebApp.");
    }
});


window.triggerFileInputClick = (element) => {
    element.click();
};

function isTelegramWebApp() {
    return window.Telegram
        && window.Telegram.WebApp
        && typeof window.Telegram.WebApp.initData === 'string'
        && window.Telegram.WebApp.initData.length > 0
        && window.Telegram.WebApp.initDataUnsafe
        && window.Telegram.WebApp.initDataUnsafe.user !== undefined;
}

window.getTelegramData = function () {
    if (!isTelegramWebApp()) {
        console.log("is not telegram app");
        return null;
    }
    return {
        initData: window.Telegram.WebApp.initData,
        user: window.Telegram.WebApp.initDataUnsafe.user
    };
}; 