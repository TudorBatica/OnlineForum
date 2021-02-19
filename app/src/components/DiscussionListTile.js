const DiscussionListTile = ({ title, description, category, career, views }) => {
    return (
        <article>
            <h2>
                {title}
            </h2>
            <p>
                {description}
            </p>
            <p>
                {category}
            </p>
            <p>
                {career}
            </p>
            <p>
                {views}
            </p>
        </article>);
}