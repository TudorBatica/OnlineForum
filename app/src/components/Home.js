import useFetch from '../hooks/useFetch';
import PostsList from './PostsList';

const Home = () => {
    const {data: tasks, isLoading, error} = useFetch('http://localhost:5000/api/discussions');
    console.log(tasks)
    return (
        <div className="home">
            {error && <div>{error}</div>}
            {isLoading && <div>Loading Tasks...</div>}
            <p>{tasks}</p>
        </div>
    );
} 
 
export default Home;

//<PostsList tasks = {tasks}/>