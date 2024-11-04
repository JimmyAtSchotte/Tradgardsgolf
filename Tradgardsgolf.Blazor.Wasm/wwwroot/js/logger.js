(function () {
    var logger = document.getElementById('app-init-logger');

    // Store original console methods
    const originalConsole = {
        log: console.log,
        info: console.info,
        warn: console.warn,
        error: console.error
    };

    function displayMessageWithFade(message) {
        logger.style.opacity = 0; // Start fade out
        setTimeout(() => {
            logger.innerHTML = message; // Set new message
            logger.style.opacity = 1; // Fade in
        }, 500); // Delay to match fade-out duration
    }

    function logToPageAndConsole(method, args) {
        // Create a combined message string
        let message = '';
        for (var i = 0; i < args.length; i++) {
            message += (typeof args[i] === 'object')
                ? (JSON && JSON.stringify ? JSON.stringify(args[i], undefined, 2) : args[i])
                : args[i];
            message += ' ';
        }

        // Display message with fade effect
        displayMessageWithFade(message);

        // Call the original console method to log in the console
        originalConsole[method](...args);
    }

    // Override console methods to log to both page and console
    console.log = function () { logToPageAndConsole('log', arguments); };
    console.info = function () { logToPageAndConsole('info', arguments); };
    console.warn = function () { logToPageAndConsole('warn', arguments); };
    console.error = function () { logToPageAndConsole('error', arguments); };
})();