export function applySeo(route) {
  const title = route?.meta?.title
  const description = route?.meta?.description

  if (typeof title === 'string' && title.trim()) document.title = title

  if (typeof description === 'string') {
    let tag = document.querySelector('meta[name="description"]')
    if (!tag) {
      tag = document.createElement('meta')
      tag.setAttribute('name', 'description')
      document.head.appendChild(tag)
    }
    tag.setAttribute('content', description)
  }
}

