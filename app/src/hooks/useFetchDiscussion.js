import useFetch from './useFetch';

const useFetchDiscusion = (id) => {
    const endpoint = process.env.REACT_APP_API_DISCUSSIONS_ENDPOINT + id;
    return useFetch(endpoint);
}

export default useFetchDiscusion;