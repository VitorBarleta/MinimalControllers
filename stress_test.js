import http from 'k6/http';

export let options = {
    insecureSkipTLSVerify: true,
    noConnectionReuse: false,
    vus: 20,
    duration: '60s'
};

export default () => {
    http.get('https://localhost:7045/customer');
}
