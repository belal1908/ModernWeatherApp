window.toggleMode = () => {
  const body = document.querySelector('body');
  if (body) {
    body.classList.toggle('dark-mode');
  }
};