window.toggleMode = function () {
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', () => {
                applyToggleMode();
            });
        } else {
            applyToggleMode();
        }
    };

    function applyToggleMode() {
        const body = document.body;
        if (!body) return;

        const isDark = body.classList.toggle('dark-mode');
        localStorage.setItem('theme', isDark ? 'dark' : 'light');
        document.documentElement.classList.add('theme-transition');
        setTimeout(() => {
            document.documentElement.classList.remove('theme-transition');
        }, 300);
    }

    window.applySavedTheme = function () {
        const savedTheme = localStorage.getItem('theme');
        if (savedTheme === 'dark') {
            document.body.classList.add('dark-mode');
        }
    };

    window.addEventListener('DOMContentLoaded', () => {
        window.applySavedTheme();
    });