const DiscussionListTile = ({ discussion }) => (
    <article>
        <h2>
            {discussion.title}
        </h2>
        <p>
            {discussion.description}
        </p>
        <p>
            {discussion.career.name}
        </p>
        <p>
            {discussion.discussionType.name}
        </p>
        <p>
            {discussion.views}
        </p>
    </article>);

export default DiscussionListTile;