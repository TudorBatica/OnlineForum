import { useState, useEffect } from 'react';

const useFetch = (endpoint, header = null) => {
    const [data, setData] = useState(null);
    const [responseHeaders, setResponseHeader] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    const handleFetchedData = (fetchedData) => {
        setIsLoading(false);
        setData(fetchedData);
        setError(null);
    }

    const handleError = (error) => {
        if (error.name !== 'AbortError') {
            setIsLoading(false);
            setError(error.message);
        } else {
            console.log("Fetch aborted");
        }
    }

    const fetchData = (url, header, abortCtrl) => {

        fetch(url, {
            signal: abortCtrl.signal,
            headers: {
                'ApiKey': process.env.REACT_APP_API_KEY,
                ...header
            }
        })
            .then(resp => {
                if (!resp.ok) {
                    throw Error('could not fetch data');
                }
                setResponseHeader(resp.headers);
                return resp.json();
            })
            .then(
                (fetchedData) => handleFetchedData(fetchedData)
            )
            .catch(error => handleError(error));
    }

    useEffect(() => {
        const abortCtrl = new AbortController();
        const url = process.env.REACT_APP_API_BASE + endpoint;

        fetchData(url, header, abortCtrl);

        return () => abortCtrl.abort();
    }, []);

    return { data, isLoading, error, responseHeaders }
}

export default useFetch;