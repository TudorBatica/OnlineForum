import useFetch from './useFetch';

const useFetchDiscussionsPage = (page, pageSize) => {
    const endpoint = `${process.env.REACT_APP_API_DISCUSSIONS_ENDPOINT}?page=${page}&pagesize=${pageSize}`;
    return useFetch(endpoint);
}

export default useFetchDiscussionsPage;