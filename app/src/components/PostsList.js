const PostsList = ({ posts }) => {
    return (
        <div className="posts-list">
            {posts?.map((post) => (
                <div className="task-preview" key={post.id}>
                    <h2>{post.title}</h2>
                    <p>{post.description}</p>
                </div>
            ))}
        </div>
    );
}

export default PostsList;