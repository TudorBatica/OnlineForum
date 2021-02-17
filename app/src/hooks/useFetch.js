import { useState, useEffect } from 'react';

const useFetch = (url) => {
    const [data, setData] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const abortCtrl = new AbortController();
        console.log(process.env.REACT_APP_API_KEY);
        fetch(url, { 
            signal: abortCtrl.signal,
            headers: {
                'ApiKey': process.env.REACT_APP_API_KEY
            }
            })
            .then(resp => {
                if (!resp.ok) {
                    throw Error('could not fetch data');
                }
                console.log("Ok!");
                return resp.json();
            })
            .then((fetchedData) => {
                setIsLoading(false);
                setData(fetchedData);
                setError(null);
            })
            .catch(error => {
                if (error.name === 'AbortError') {
                    console.log('fetch aborted');
                } else {
                    setIsLoading(false);
                    setError(error.message);
                }
            });

        return () => abortCtrl.abort();
    }, [url]);

    return { data, isLoading, error }
}

export default useFetch;